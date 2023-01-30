using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace plot_utils
{
    public class MyGLoader : GLoader
    {
        override protected void LoadExternal()
        {
            /*
            开始外部载入，地址在url属性
            载入完成后调用OnExternalLoadSuccess
            载入失败调用OnExternalLoadFailed
            注意：如果是外部载入，在载入结束后，调用OnExternalLoadSuccess或OnExternalLoadFailed前，
            比较严谨的做法是先检查url属性是否已经和这个载入的内容不相符。
            如果不相符，表示loader已经被修改了。
            这种情况下应该放弃调用OnExternalLoadSuccess或OnExternalLoadFailed。
            */

            //这里使用成员变量url查找Sprite载入即可,注意FGUI的图集和Unity中的图集Y轴相反，需要重新计算Rect才能正确加载
            if (url.Length > 0)
            {
                Debug.Log("MyGLoader is running!");
                Sprite[] tSprites = Resources.LoadAll<Sprite>(url);
                Sprite tSprite = tSprites[0];
                // 反转Y轴 
                Rect tShowRect = new Rect(tSprite.textureRect.x, tSprite.texture.height - tSprite.textureRect.y - tSprite.textureRect.height,
                        tSprite.textureRect.width, tSprite.textureRect.height);
                this.onExternalLoadSuccess(new NTexture(tSprite.texture, tShowRect));
            }

            this.onExternalLoadFailed();
        }


        override protected void FreeExternal(NTexture texture)
        {
            //释放外部载入的资源
        }
    }

}
