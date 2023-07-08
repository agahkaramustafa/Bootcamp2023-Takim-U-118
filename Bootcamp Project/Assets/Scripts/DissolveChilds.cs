using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DissolveExample
{
    public class DissolveChilds : MonoBehaviour
    {

        // Start is called before the first frame update
        List<Material> materials = new List<Material>();
        bool PingPong = false;
        void Start()
        {
            var renders = GetComponentsInChildren<Renderer>();
            for (int i = 0; i < renders.Length; i++)
            {
                materials.AddRange(renders[i].materials);
            }
        }

        private void Reset()
        {
            Start();
            SetValue(0);
        }

        public void SetValue(float value)
        {
            for (int i = 0; i < materials.Count; i++)
            {
                materials[i].SetFloat("_Dissolve", value);
            }
        }

        public void Dissolve()
        {
            StartCoroutine(DissolveRoutine());
        }

        public void Respawn()
        {
            StartCoroutine(RespawnRoutine());
        }

        IEnumerator DissolveRoutine()
        {
            float t = 0;

            while (true)
            {
                var value = Mathf.Lerp(0f, 1f, t);
                SetValue(value);
                if (value >= .98f)
                {
                    value = 1f;
                    SetValue(value);
                    break;
                }
                else
                {
                    yield return null;
                }
                t += 0.5f * Time.deltaTime;
            }
        }

        IEnumerator RespawnRoutine()
        {
            float t = 0;

            while (true)
            {
                var value = Mathf.Lerp(1f, 0f, t);
                SetValue(value);
                if (value <= .02f)
                {
                    value = 0f;
                    SetValue(value);
                    break;
                }
                else
                {
                    yield return null;
                }
                t += 0.5f * Time.deltaTime;
            }
        }
    }
}