using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.SceneManagement.SceneManager;

namespace Script.Runtime.SideKick {
    public class SCSideKickTalk : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI _dialogueBox;

        [SerializeField] GameObject _dialBoxBackground;
        [SerializeField] List<string> _dialogue;
        [SerializeField] List<string> _alternateDialogue;
        [SerializeField] float _timeBetweenText;
        
        private Coroutine _buildProcess;
        
        private int _oldTextLength;
        private string _oldText;
        string _newText;
        [SerializeField] private bool CouldAppendText;
        
        bool _isBuildingText => _buildProcess != null;

        [SerializeField] private int CharacterPerCycle = 2;
        
        [SerializeField] int _nbOfLines = 2;

        private bool _isTextShowed;

        private void Start() {
            _dialBoxBackground.SetActive(false);
        }

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Player")) {
                if(_isTextShowed) return;
                _dialBoxBackground.SetActive(true);
                if (SCProphecyManager.Instance.IsAllMemoryPiecesCollected) {
                    StartCoroutine(BuildLines(_alternateDialogue));
                }
                StartCoroutine(BuildLines(_dialogue));
                _isTextShowed = true;
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.gameObject.CompareTag("Player")) {
                //if(_isBuildingText) return;
                //_dialBoxBackground.SetActive(false);
            }
        }

        IEnumerator BuildLines(List<string> lines) {
            foreach (string line in lines) {
                yield return BuildDialogue(line);
                yield return new WaitForSeconds(_timeBetweenText);
            }
        }
        
        IEnumerator BuildDialogue(string line) {
            if (_dialogueBox.text.Split("\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Length > _nbOfLines-1 || !CouldAppendText) {
                BuildText(line);
            }
            else {
                if (_dialogueBox.text != "") {
                    AppendText("\n" + line);
                }
                else {
                    AppendText(line);
                }
            }

            while (_isBuildingText) {
                yield return null;
            }
         
        }
        
        Coroutine BuildText(string text) {
            _oldText = "";
            _newText = text;
            StopBuild();
            _buildProcess = _dialogueBox.StartCoroutine(Building());
            return _buildProcess;
        }
        
        Coroutine AppendText(string text) {
            _oldText = _dialogueBox.text;
            _newText = text;
            StopBuild();
            _buildProcess = _dialogueBox.StartCoroutine(Building());
            return _buildProcess;
        }

        IEnumerator Building() {
            Prepare();

            yield return Build();
            
            _buildProcess = null;
        }

        void Prepare() {
            _dialogueBox.text = _oldText;
            if (_oldText != "") {
                _dialogueBox.ForceMeshUpdate();
                _oldTextLength = _dialogueBox.text.Length;
            }
            else {
                _oldTextLength = 0;
            }
            
            
            _oldTextLength = _dialogueBox.text.Length;
            _dialogueBox.text += _newText;
            _dialogueBox.maxVisibleCharacters = int.MaxValue;
            _dialogueBox.ForceMeshUpdate();
            
            TMP_TextInfo textInfo = _dialogueBox.textInfo;
            Color colorVisible = new(_dialogueBox.color.r, _dialogueBox.color.g, _dialogueBox.color.b, 1f);
            Color colorInvisible = new(_dialogueBox.color.r, _dialogueBox.color.g, _dialogueBox.color.b, 0f);
            
            Color32[] vertexColors = textInfo.meshInfo[textInfo.characterInfo[0].materialReferenceIndex].colors32;

            for (int u = 0; u < textInfo.characterCount; u++) {
                TMP_CharacterInfo charInfo = textInfo.characterInfo[u];
                
                if(!charInfo.isVisible) continue;

                if (u < _oldTextLength) {
                    for (int v = 0; v<4; v++) {
                        vertexColors[charInfo.vertexIndex+v] = colorVisible;
                    }
                }
                else {
                    for (int v = 0; v < 4; v++) {
                        vertexColors[charInfo.vertexIndex + v] = colorInvisible;
                    }
                }
            }

            _dialogueBox.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
        }

        IEnumerator Build() {
            int minRange = _oldTextLength;
            int maxRange = minRange + 1;

            byte alphaMax = 15;
            
            TMP_TextInfo textInfo = _dialogueBox.textInfo;
            
            Color32[] vertexColors = textInfo.meshInfo[textInfo.characterInfo[0].materialReferenceIndex].colors32;
            float[] alphas = new float[textInfo.characterCount];

            while (true) {
                for(int u = minRange; u < maxRange; u++) {
                    TMP_CharacterInfo charInfo = textInfo.characterInfo[u];
                    
                    if(!charInfo.isVisible) continue;  
                    
                    int vertexIndex = charInfo.vertexIndex;
                    alphas[u] = Mathf.MoveTowards(alphas[u], 255, CharacterPerCycle);

                    for (int v = 0; v < 4; v++) {
                        vertexColors[vertexIndex + v].a = (byte)alphas[u];
                    }
                    if (alphas[u] >= 255) { minRange++; }
                }
                
                _dialogueBox.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
                
                bool isLastCharVisible = !textInfo.characterInfo[maxRange- 1].isVisible;
                if(alphas[maxRange-1] > alphaMax || isLastCharVisible) {
                    if(maxRange<textInfo.characterCount) maxRange++;
                    else if(alphas[maxRange-1] >= 255 || isLastCharVisible) {
                        break;
                    }
                }

                yield return new WaitForEndOfFrame();
            }
        }
        
        void StopBuild() {
            if (!_isBuildingText) {
                return;
            }

            _dialogueBox.StopCoroutine(_buildProcess);
            _buildProcess = null;
        }
    }

}