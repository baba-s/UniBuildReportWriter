# UniBuildReportWriter

ビルド開始時に BuildReport を JSON 形式で Resources フォルダ内に保存するエディタ拡張

## 使用例

```cs
using UnityEngine;

public class Example : MonoBehaviour
{
    private string m_buildReport;

    private void Awake()
    {
        var textAsset = Resources.Load<TextAsset>( "build_report" );
        m_buildReport = textAsset != null ? textAsset.text : string.Empty;
    }

    private void OnGUI()
    {
        GUILayout.Label( m_buildReport );
    }
}
```

このパッケージを Unity プロジェクトに導入した状態でビルドすると  
Resources フォルダ内に build_report.txt というファイル名で Build Report が書き込まれるので  
Resources.Load<TextAsset> で読み込むことでゲーム実行中に Build Report を参照できます  
