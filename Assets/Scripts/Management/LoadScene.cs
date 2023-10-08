///<summary>
/// Script by The Seeker Of Virtue
/// Load a scene, with a loading screen.
///</summary>

using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Carto.Load {

    public class LoadScene : MonoBehaviour {

        static bool isLoading = false;

        ///<summary>
        /// Load a scene by name.
        ///</summary>
        /// <param name="levelName"> Name of the level to load </param>
        public async static void Load (string levelName = "") {

            if (isLoading)
                return;

            isLoading = true;

            SpawnLoadScreen ();

            AsyncOperation operation = SceneManager.LoadSceneAsync (levelName, LoadSceneMode.Single);

            operation.allowSceneActivation = false;
            float startTime = Time.time;

            while (!operation.isDone) {
                //must wait a little. 
                if (Time.time - startTime > 1)
                    operation.allowSceneActivation = true;

                await Task.Yield ();
            }

            DestroyLoadScreen ();

            isLoading = false;

        }

        ///<summary>
        /// Reload current scene
        ///</summary>
        public static void Reload () {
            Load (SceneManager.GetActiveScene ().name);
        }

        static LoadScreen loadScreenObj;
        static GameObject loadScreenPrefab;
        static async void SpawnLoadScreen () {
            //wait just in case the loading screen has not yet been loaded
            while (loadScreenPrefab == null)
                await Task.Yield ();
            loadScreenObj = Instantiate (loadScreenPrefab, Vector3.zero, Quaternion.identity).GetComponent<LoadScreen> ();

        }

        [RuntimeInitializeOnLoadMethod]
        static async void LoadLoadscreenPrefab () {
            //load in the loading screen.. heh ironic
            var request = Resources.LoadAsync<GameObject> ("Load Screen");
            while (!request.isDone) {
                await Task.Yield ();
            }
            loadScreenPrefab = request.asset as GameObject;
        }

        static async void DestroyLoadScreen () {
            if (loadScreenObj != null) {
                loadScreenObj.DestroyMe ();
                loadScreenObj = null;
            } else {
                //in case the level loaded faster than the loading screen. Just in case lmao. 
                while (loadScreenObj == null) {
                    await Task.Yield ();
                }
                print ("Just saying, the level legit loaded faster than the loading screen. I'm serious. This is incredibly unlikely to say the least.");
                Destroy (loadScreenObj);
                loadScreenObj = null;
            }
        }

    }

}