using Cinemachine;
using System.Collections;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public string[] lines;
    public float textSpeed = 0.1f;
    int index;
    private CinemachineBrain cinemachineBrain;
    public Camera mainCamera;
    public float zoomedOrthoSize = 5f;
    private float originalOrthoSize;
    public float zoomDuration = 2f;
    private bool cameraMoving = false;
    private Vector3 originalCameraPosition;

    void Start()
    {
        dialogueText.text = string.Empty;
        cinemachineBrain = mainCamera.GetComponent<CinemachineBrain>();
        originalCameraPosition = mainCamera.transform.position;
        originalOrthoSize = mainCamera.orthographicSize;
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !cameraMoving)
        {
            if (dialogueText.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = lines[index];
            }
        }
    }

    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(SwitchToManualCameraControl());
        StartCoroutine(WriteLine());
    }

    IEnumerator WriteLine()
    {
        foreach (char letter in lines[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(WriteLine());
        }
        else
        {
            StartCoroutine(SwitchBackToCinemachine());
            StartCoroutine(DeactivateDialoguePanel());
        }
    }


    IEnumerator SwitchToManualCameraControl()
    {
        cameraMoving = true;
        cinemachineBrain.enabled = false;
        float elapsed = 0f;
        while (elapsed < zoomDuration)
        {
            mainCamera.orthographicSize = Mathf.Lerp(originalOrthoSize, zoomedOrthoSize, elapsed / zoomDuration);
            mainCamera.transform.position = Vector3.Lerp(originalCameraPosition, new Vector3(2.26f, -1.72f, originalCameraPosition.z), elapsed / zoomDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        cameraMoving = false;
    }

    IEnumerator SwitchBackToCinemachine()
    {
        float elapsed = 0f;
        Vector3 targetPosition = originalCameraPosition;
        float targetSize = originalOrthoSize;

        while (elapsed < zoomDuration)
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetSize, elapsed / zoomDuration);
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, elapsed / zoomDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        cinemachineBrain.enabled = true;
    }

    IEnumerator DeactivateDialoguePanel()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}

