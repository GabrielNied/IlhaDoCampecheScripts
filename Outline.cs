/*
//  Copyright (c) 2015 José Guerreiro. All rights reserved.
//
//  MIT license, see http://www.opensource.org/licenses/mit-license.php
//  
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//  
//  The above copyright notice and this permission notice shall be included in
//  all copies or substantial portions of the Software.
//  
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//  THE SOFTWARE.
*/

using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace cakeslice
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Renderer))]
    public class Outline : MonoBehaviour
    {
        public Renderer Renderer { get; private set; }

        public int color;
        public bool eraseRenderer;

        [HideInInspector]
        public int originalLayer;
        [HideInInspector]
        public Material[] originalMaterials;


        public bool mouseOver = false;

        public Image mouse;

        private void Awake()
        {
            Renderer = GetComponent<Renderer>();            
        }

        private void Start()
        {
            if (GameObject.FindGameObjectWithTag("Mouse") != null)
            {
                mouse = GameObject.FindGameObjectWithTag("Mouse").GetComponent<Image>();
                mouse.enabled = false;
            }
        }

        void OnEnable()
        {
            IEnumerable<OutlineEffect> effects = Camera.allCameras.AsEnumerable()
                .Select(c => c.GetComponent<OutlineEffect>())
                .Where(e => e != null);

            foreach (OutlineEffect effect in effects)
            {
                effect.AddOutline(this);
            }
        }

        void OnDisable()
        {
            IEnumerable<OutlineEffect> effects = Camera.allCameras.AsEnumerable()
                .Select(c => c.GetComponent<OutlineEffect>())
                .Where(e => e != null);

            foreach (OutlineEffect effect in effects)
            {
                effect.RemoveOutline(this);
            }
        }

        public void Update()
        {
            if (SceneManager.GetActiveScene().name == "Ilha")
            {
                Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                RaycastHit hitInfo;

                if (Physics.Raycast(rayOrigin, out hitInfo, 2))
                {
                    if (hitInfo.transform.tag == "MagicRock" || hitInfo.transform.tag == "Runa Azul" || hitInfo.transform.tag == "Runa verde" || hitInfo.transform.tag == "Runa Amarela" || hitInfo.transform.tag == "Runa Rosa" || hitInfo.transform.tag == "Runa Roxa")
                    {
                        eraseRenderer = false;
                        mouse.enabled = true;
                    }
                    else
                    {
                        eraseRenderer = true;
                        mouse.enabled = false;
                    }
                }
                else
                {
                    eraseRenderer = true;
                    mouse.enabled = false;
                }
            }
        }
        
        public void OnMouseEnter()
        {
            mouseOver = true;
        }
        public void OnMouseOver()
        {
            mouseOver = true;
        }
        public void OnMouseExit()
        {
            mouseOver = false;
        }
        
        public void OnTriggerEnter(Collider other)
        {
            if (SceneManager.GetActiveScene().name == "Museu")
            {
                if (!GetComponent<ChamaHistoria>().espera && !GetComponent<ChamaHistoria>().tocou && mouseOver)
                {
                    eraseRenderer = false;
                    mouse.enabled = true;
                }
                else
                {
                    eraseRenderer = true;
                    mouse.enabled = false;
                }
            }
        }
        public void OnTriggerStay(Collider other)
        {
            if (SceneManager.GetActiveScene().name == "Museu")
            {
                if (!GetComponent<ChamaHistoria>().espera && !GetComponent<ChamaHistoria>().tocou && mouseOver)
                {
                    eraseRenderer = false;
                    mouse.enabled = true;
                }
                else
                {
                    eraseRenderer = true;
                    mouse.enabled = false;
                }
            }
        }
        public void OnTriggerExit(Collider other)
        {
            if (SceneManager.GetActiveScene().name == "Museu")
            {
                eraseRenderer = true;
                mouse.enabled = false;
            }
        }
    }
}