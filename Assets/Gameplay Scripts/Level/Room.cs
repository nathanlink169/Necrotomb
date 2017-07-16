using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : BaseBehaviour
{
    [SerializeField] private List<Room> m_RoomsToUpdate = new List<Room>();
    [SerializeField] private List<Room> m_ReferencesForActive = new List<Room>();

    void Update()
    {
        if(m_RoomsToUpdate.Contains(this) == false)
        {
            m_RoomsToUpdate.Add(this);
        }

        if(m_ReferencesForActive.Count > 0)
        {
            if(gameObject.activeInHierarchy == false)
            {
                gameObject.SetActive(true);
            }
        }

        else
        {
            if(gameObject.activeInHierarchy == true)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void AddRoomToReferenceList(Room aRoom)
    {
        if (m_ReferencesForActive.Contains(aRoom))
        {
            return;
        }

        m_ReferencesForActive.Add(aRoom);
    }

    public void RemoveRoomFromReferenceList(Room aRoom)
    {
        while (m_ReferencesForActive.Contains(aRoom))
        {
            m_ReferencesForActive.Remove(aRoom);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < m_RoomsToUpdate.Count; i++)
            {
                m_RoomsToUpdate[i].AddRoomToReferenceList(this);
                m_RoomsToUpdate[i].Update();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < m_RoomsToUpdate.Count; i++)
            {
                m_RoomsToUpdate[i].RemoveRoomFromReferenceList(this);
            }
        }
    }
}