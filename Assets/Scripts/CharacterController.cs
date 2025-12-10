using System.Collections;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private SkinnedMeshRenderer character;

    public float sensitivity = 200f;

    private float[] samples = new float[1024];

    public float smoothness = 50f;

    private float weight;

    public int smileIndex;

    public int sadIndex;

    public float animTime = 0.5f;

    private void Start()
    {
        StartCoroutine(InitializeCharacterSpeech());
    }

    void Update()
    {
        // If the audio source is NOT playing any audio
        if (!audioSource.isPlaying)
        {
            // Reset blendshape to neutral
            character.SetBlendShapeWeight(16, 0);
            return;
        }

        // Read current audio sample data from channel 0 (left channel)
        audioSource.GetOutputData(samples, 0);

        // Calculate RMS value of the audio samples (volume intensity)
        float sum = 0f;
        for (int i = 0; i < samples.Length; i++)
            sum += samples[i] * samples[i]; // square each sample

        // calculate overall loudness
        float rmsValue = Mathf.Sqrt(sum / samples.Length);

        // Convert loudness to a 0–1 normalized value
        float value = Mathf.Clamp01(rmsValue * sensitivity);

        // Smoothly interpolate current weight toward the target value
        weight = Mathf.Lerp(weight, value * 100f, Time.deltaTime * smoothness);

        // Apply final weight to the blendshape (index 16)
        character.SetBlendShapeWeight(16, weight);
    }

    private IEnumerator InitializeCharacterSpeech()
    {
        yield return new WaitForSeconds(1f);

        audioSource.Play();
    }

    public void ChangeExpression()
    {
        if (audioSource.isPlaying) return;

        StartCoroutine(IChangeExpression());
    }

    private IEnumerator IChangeExpression()
    {
        for (int i = 0; i < 4; i++)
        {
            // choose wihich animation to play
            int index = i % 2 == 0 ? smileIndex : sadIndex;

            float time = 0;
            while (time <= animTime)
            {
                time += Time.deltaTime;
                float t = time / animTime;

                float weight = Mathf.Lerp(0, 100, t);

                character.SetBlendShapeWeight(index, weight);

                yield return null;
            }

            // complete the current animation
            character.SetBlendShapeWeight(index, 100);

            yield return new WaitForSeconds(1f);

            // reset the current animation
            character.SetBlendShapeWeight(index, 0);

            yield return new WaitForSeconds(1f);
        }
    }
}