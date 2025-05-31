using UnityEngine;
using UnityEngine.Serialization;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject m_menuCanvas;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_menuCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ToggleMenu();
    }

    private void ToggleMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_menuCanvas.SetActive(!m_menuCanvas.activeSelf);
        }   
    }
}
