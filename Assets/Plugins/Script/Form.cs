#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;
using System;
using System.IO;

namespace Test
{
    public sealed class Form : MonoBehaviour
    {
        [SerializeField] InputField[] Fields;
        [SerializeField] Button Btn;
        string PACKEGE_PATH;
        bool NowCreating;

        [Serializable]
        class SendData
        {
            public string Name;
            public string Old;
            public string Url;
            public string ID;
        }

        SendData Data;

		void Awake()
		{
            PACKEGE_PATH = Application.dataPath + "/../Assets/test.unitypackage";
		}

		void Start()
        {
            Btn.onClick.AddListener(OnClick);
        }

        void OnClick()
        {
            if (ValidateFields(Fields))
            {
                Data = new SendData()
                {
                    Name = Fields[0].text,
                    Old = Fields[1].text,
                    Url = Fields[2].text,
                };
                ExportPackeage.Export(PACKEGE_PATH);
                NowCreating = true;
            }
        }

        void Update()
        {
            if(NowCreating && File.Exists(PACKEGE_PATH))
            {
                NowCreating = false;
                StartCoroutine(SendServer());
            }
        }

        bool ValidateFields(InputField[] fields)
        {
            var s = true;
            for (int i = 0; i < fields.Length - 1; i++)
            {
                if(string.IsNullOrEmpty(fields[i].text)) s = false;
            }
            if (!s) EditorUtility.DisplayDialog("Input error", "Điền Tên, Tuổi (yêu cầu bắt buộc)", "Ok");
            return s;
        }

        void OnProgress(float current)
        {
            EditorUtility.DisplayProgressBar("Uploading", "Dự án đang được tải lên, xin vui lòng chờ đợi", current);
        }

        IEnumerator SendServer()
        {
            var form = new WWWForm();
            Data.ID = Guid.NewGuid().ToString();

            form.AddField("info", JsonUtility.ToJson(Data));

            var bytes = File.ReadAllBytes(PACKEGE_PATH);
            if (bytes != null)
            {
                form.AddBinaryData("unitypackage", bytes, "test.unitypackage", "application/octet-stream");
            }

            var uploader = UploadServer.Create();
            uploader.OnComplete += (bool isSuccsess, string msg) =>
            {
                if(isSuccsess) EditorUtility.DisplayDialog("Done","Mã xác nhận : 「" + Data.ID + "」\nCảm ơn bạn đã tham gia bài test. Hãy gửi mã xác nhận cho bộ phận tuyển dụng của chúng tôi hoặc gửi đến tuyendung@mirabo.com.vn. Best Regard!", "Ok");
                else EditorUtility.DisplayDialog("Gửi file thất bại", $"Thất bại trong việc gửi từ .Assets / test.unitypackage, hãy thử gửi lại. {msg}", "Close");

                EditorUtility.ClearProgressBar();
            };
            uploader.OnNowProgress += OnProgress;

            yield return StartCoroutine(uploader.Send(form));
        }

    }
}

#endif