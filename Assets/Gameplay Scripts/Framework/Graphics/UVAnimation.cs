using UnityEngine;
using System.Collections;

namespace GameFramework
{
    public class UVAnimation : BaseBehaviour
    {
        public int uvTileY = 4; // texture sheet columns 
        public int uvTileX = 4; // texture sheet rows

        public int fps = 30;

        #region BaseBehaviour
        private void Start()
        {
            m_renderer = GetComponent<Renderer>();
        }

        private void Update()
        {
            //calculate the index
            m_index = (int)(Time.time * fps);

            //repeat when when exhausting all frames
            m_index = m_index % (uvTileY * uvTileX);

            //size of each tile  
            m_size = new Vector2(1.0f / uvTileY, 1.0f / uvTileX);

            //split into horizontal and vertical indexes
            var uIndex = m_index % uvTileX;
            var vIndex = m_index / uvTileX;

            //build the offset   
            //v coordinate is at the bottom of the image in openGL, so we invert it
            m_offset = new Vector2(uIndex * m_size.x, 1.0f - m_size.y - vIndex * m_size.y);

            m_renderer.material.SetTextureOffset("_MainTex", m_offset);
            m_renderer.material.SetTextureScale("_MainTex", m_size);
        }
        #endregion

        #region Private
        private int m_index;

        private Vector2 m_size;
        private Vector2 m_offset;

        private Renderer m_renderer;
        #endregion
    }
}
