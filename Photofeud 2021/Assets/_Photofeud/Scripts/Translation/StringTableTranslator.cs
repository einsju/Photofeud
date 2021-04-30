using System.Collections;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;

namespace Photofeud.Translation
{
    public class StringTableTranslator : MonoBehaviour, ITranslator
    {
        const string Error = "error";
        
        [SerializeField] LocalizedStringTable localizedStringTable;

        StringTable _stringTable;

        IEnumerator Start()
        {
            var tableLoading = localizedStringTable.GetTable();
            yield return tableLoading;

            _stringTable = tableLoading.Result;
        }

        public string Translate(string key)
        {
            var entry = _stringTable.GetEntry(key);
            return entry != null ? entry.LocalizedValue : _stringTable.GetEntry(Error).LocalizedValue;
        }
    }
}
