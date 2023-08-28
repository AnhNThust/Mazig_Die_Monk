using System.Collections;
using UnityEngine;

namespace Test
{
    public sealed class UploadServer : MonoBehaviour
	{
		public event System.Action<float> OnNowProgress = delegate{};
		public event System.Action<bool, string> OnComplete = delegate{};

		float currentProgress = 0;

		public static UploadServer Create()
		{
			var go = new GameObject("uploadServer");
			DontDestroyOnLoad(go);
			var script = go.AddComponent<UploadServer>();

			return script;
		}

		public IEnumerator Send(WWWForm form)
		{
            using(WWW www = new WWW( "http://27.72.97.102:8080/unity_test/", form ))
			{
				while (!www.isDone)
				{
					if(currentProgress != www.uploadProgress
						&& OnNowProgress != null)
					{
						currentProgress = www.uploadProgress;
						OnNowProgress(currentProgress);
					}

					yield return null;
				}

				if(OnComplete != null)
				{
					OnNowProgress(1f);
					//エラー処理
					if (!string.IsNullOrEmpty(www.error)) OnComplete (false, www.error);
					//完了処理
					else OnComplete(true, www.text);
				}
			}

			Destroy(gameObject);
		}

	}
}