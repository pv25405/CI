using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Collections;
using System.Collections.Generic;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    

    [Header("Sons")]
    public AudioClip soundAcerto;
    public AudioClip soundErro;
    private AudioSource audioSource;
public string categoriaCorreta;

    private Vector3 posInicial;
    private Image img;
    private Vector3 zonaFinalPos;
    private bool jaColocado = false;


void Start()
    {
        posInicial = transform.position;
        img = GetComponent<Image>();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

public void OnBeginDrag(PointerEventData eventData)
    {
        if (jaColocado) return;
        img.color = Color.white;
    }

public void OnDrag(PointerEventData eventData)
    {
        if (jaColocado) return;
        transform.position = eventData.position;
    }

public void OnEndDrag(PointerEventData eventData)
    {
        if (jaColocado) return;

        List<RaycastResult> resultados = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, resultados);

        foreach (RaycastResult r in resultados)
        {
            ZonaCategoria zona = r.gameObject.GetComponent<ZonaCategoria>();

            if (zona != null)
            {
                if (zona.categoria == categoriaCorreta)
                {
                    zonaFinalPos = zona.transform.position;
                    Acertou();
                    return;
                }
                else
                {
                    Errou();
                    return;
                }
            }
        }

        Errou();
    }

void Acertou()
    {
        ScoreManager.acertos++;
        PlaySound(soundAcerto);
        jaColocado = true;
        StartCoroutine(AnimacaoAcerto());
        VerificarFimDeJogo();
    }

void Errou()
    {
        ScoreManager.erros++;
        PlaySound(soundErro);
        StartCoroutine(AnimacaoErro());
    }

    IEnumerator AnimacaoAcerto()
    {
        img.color = Color.green;
        yield return new WaitForSeconds(0.4f);
        img.color = Color.white;

        transform.position = zonaFinalPos;
    }

    IEnumerator AnimacaoErro()
    {
        img.color = Color.red;

        for (int i = 0; i < 5; i++)
        {
            transform.position += new Vector3(10, 0, 0);
            yield return new WaitForSeconds(0.05f);
            transform.position -= new Vector3(10, 0, 0);
            yield return new WaitForSeconds(0.05f);
        }

        img.color = Color.white;
        transform.position = posInicial;
    }


void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }


void VerificarFimDeJogo()
    {
        DragDrop[] todos = FindObjectsByType<DragDrop>(FindObjectsSortMode.None);
        foreach (DragDrop d in todos)
        {
            if (!d.jaColocado) return;
        }

        TempoCronometroTMP cronometro = FindFirstObjectByType<TempoCronometroTMP>();
        if (cronometro != null)
        {
            SceneManager.LoadScene(cronometro.cenaResultado);
        }
    }
}
