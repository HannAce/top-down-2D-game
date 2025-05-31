using UnityEngine;
using UnityEngine.UI;

public class MenuTabManager : MonoBehaviour
{
    [SerializeField] private Image[] m_tabImages;
    [SerializeField] private GameObject[] m_pages;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ActivateTab(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateTab(int tabIndex)
    {
        for (int i = 0; i < m_tabImages.Length; i++)
        {
            m_pages[i].SetActive(false);
            m_tabImages[i].color = Color.grey;
        }
        
        m_pages[tabIndex].SetActive(true);
        m_tabImages[tabIndex].color = Color.white;
    }
}
