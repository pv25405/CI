using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string categoriaCorreta;

    private Vector3 posInicial;
    private Image img;
    private Vector3 zonaFinalPos;

    void Start()
    {
        posInicial = transform.position;
        img = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        img.color = Color.white;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
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
        StartCoroutine(AnimacaoAcerto());
    }

    void Errou()
    {
        ScoreManager.erros++;
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
}
