﻿namespace Plugin.AppShortcuts.Android
{
    internal class AddIconData : IconData
    {
        internal override string Name => nameof(AddIconData);
        internal override string SvgData =>
            "<?xml version=\"1.0\" encoding=\"utf-8\"?><vector xmlns:android=\"http://schemas.android.com/apk/res/android\" android:width=\"48dp\" android:height=\"48dp\" android:viewportWidth=\"48\" android:viewportHeight=\"48\"><group><path android:fillColor=\"#F5F5F5\" android:fillType=\"evenOdd\" android:pathData=\"M2,24a22,22 0 0,1 44,0a22,22 0 0,1 -44,0z\"/><path android:fillColor=\"#212121\" android:pathData=\"M31 25h-6v6h-2v-6h-6v-2h6v-6h2v6h6z\"/></group></vector>";

    }
}
