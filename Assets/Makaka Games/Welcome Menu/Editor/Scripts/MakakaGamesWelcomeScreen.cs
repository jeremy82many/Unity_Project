using System;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

[InitializeOnLoad]
public class MakakaGamesWelcomeScreen:EditorWindow
{

	private static string windowHeaderText = "Welcome to Makaka Games";
	private const string PREFSHOWATSTARTUP = "MakakaGames.ShowWelcomeScreen";
	private string copyright = "© Copyright " + DateTime.Now.Year + " Makaka Games";
	private static bool showAtStartup = true;
	private Vector2 scrollPosition;
	private static bool inited;
	private static GUIStyle headerStyle;
    private static Texture2D moreAssetsTexture;
	private static Texture2D docTexture;
	private static Texture2D youTubeTexture;
	private static Texture2D vkTexture;
	private static Texture2D facebookTexture;
	private static Texture2D supportTexture;
	private static Texture2D instagramTexture;

	private static Texture2D twitterTexture;
	private static GUIStyle copyrightStyle;
	private static MakakaGamesWelcomeScreen wnd;

	private static Vector2 windowsSize = new Vector2(500f, 300f);

	static MakakaGamesWelcomeScreen()
	{
		EditorApplication.update -= GetShowAtStartup;
		EditorApplication.update += GetShowAtStartup;
	}

	private static bool DrawButton(Texture2D texture, string title, string body = "", int space = 10)
	{
		GUILayout.BeginHorizontal();

		GUILayout.Space(34f);
		GUILayout.Box(texture, GUIStyle.none, GUILayout.MaxWidth(48f), GUILayout.MaxHeight(48f));
		GUILayout.Space(10f);

		GUILayout.BeginVertical();
		GUILayout.Space(1f);
		GUILayout.Label(title, EditorStyles.boldLabel);
		GUILayout.Label(body);
		GUILayout.EndVertical();

		GUILayout.EndHorizontal();

		Rect rect = GUILayoutUtility.GetLastRect();
		EditorGUIUtility.AddCursorRect(rect, MouseCursor.Link);

		bool returnValue = Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition);

		GUILayout.Space(space);

		return returnValue;
	}

	private static void GetShowAtStartup()
	{
		EditorApplication.update -= GetShowAtStartup;
		showAtStartup = EditorPrefs.GetBool(PREFSHOWATSTARTUP, true);

		if (showAtStartup)
		{
			EditorApplication.update -= OpenAtStartup;
			EditorApplication.update += OpenAtStartup;
		}
	}

	private static bool Init()
	{
		try
		{
			headerStyle = new GUIStyle();
			headerStyle.normal.background = Resources.Load("HeaderLogo") as Texture2D;
			headerStyle.normal.textColor = Color.white;
			headerStyle.fontStyle = FontStyle.Bold;
			headerStyle.padding = new RectOffset(340, 0, 27, 0);
			headerStyle.margin = new RectOffset(0, 0, 0, 0);

			copyrightStyle = new GUIStyle();
			copyrightStyle.alignment = TextAnchor.MiddleRight;

			docTexture = Resources.Load("Docs") as Texture2D;
			moreAssetsTexture = Resources.Load("AR") as Texture2D;
			supportTexture = Resources.Load("Support") as Texture2D;
			youTubeTexture = Resources.Load("YouTube") as Texture2D;
			facebookTexture = Resources.Load("Facebook") as Texture2D;
			vkTexture = Resources.Load("VK") as Texture2D;
			instagramTexture = Resources.Load("Instagram") as Texture2D;
			twitterTexture = Resources.Load("Twitter") as Texture2D;
			
			inited = true; 
		}
		catch (Exception e)
		{
			Debug.Log("WELCOME INIT: " + e);
			return false;
		}
		return true;
	}

	private void OnEnable()
	{
		wnd = this;
	}

	private void OnDestroy()
	{
		wnd = null;
		EditorPrefs.SetBool(PREFSHOWATSTARTUP, false);
	}

	private void OnGUI()
	{
		if (!inited) Init();

		if (GUI.Button(new Rect(0f, 0f, windowsSize.x, 58f), "", headerStyle))
			Process.Start("https://makaka.org");

		GUILayoutUtility.GetRect(position.width, 70f);

		scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

		if (DrawButton(docTexture, "Docs", "Online version of the documentation.")) 
			Process.Start("https://makaka.org/category/docs/");
		if (DrawButton(supportTexture, "Support", "First of all, read the docs. If it didn't help, email us.")) 
			Process.Start("mailto:info@makaka.org?subject=Makaka Games - Asset Support");
		if (DrawButton(moreAssetsTexture, "Augmented Reality", "Our AR assets. Create your own Pokemon GO.")) 
			Process.Start("http://u3d.as/kPA");
		if (DrawButton(youTubeTexture, "YouTube Channel", "Our video materials.")) 
			Process.Start("https://www.youtube.com/makakaorg");
		if (DrawButton(facebookTexture, "Facebook page", "Our news page in English.")) 
			Process.Start("https://www.facebook.com/makakaorg");
		if (DrawButton(vkTexture, "VK page", "Our news page in Russian.")) 
			Process.Start("https://vk.com/makakaorg");
		if (DrawButton(instagramTexture, "Instagram page", "Our photos.")) 
			Process.Start("https://www.instagram.com/makakaorg/");
		if (DrawButton(twitterTexture, "Twitter page", "Our news page in English.")) 
			Process.Start("https://www.twitter.com/makakaorg/");
		
		EditorGUILayout.EndScrollView();
		EditorGUILayout.LabelField(copyright, copyrightStyle);
	}

	private static void OpenAtStartup()
	{
		if (!inited && !Init()) 
			return;

		OpenWindow();

		EditorApplication.update -= OpenAtStartup;
	}

	[MenuItem("Makaka Games/Welcome Screen", false)]
	public static void OpenWindow()
	{
		if (wnd != null) 
			return;

		wnd = GetWindow<MakakaGamesWelcomeScreen> (true, windowHeaderText, true);
		wnd.maxSize = wnd.minSize = windowsSize;
	}
}