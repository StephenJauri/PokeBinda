   M S B A M L   `   `   `  ????L  HPresentationLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:PresentationLayerPresentationLayer  5      !    PresentationLayer.TabButton   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:PresentationLayer     5      .??5
      ?   ? RowDefinitions  5   
   ?? 5      $??85 ?6   ?? 5      .+?5   
   ?? -   5        $??	Clickable??5   ?   $??0??6   $??0??6   $??70??6.   $??70??6:   $??Center=?6E   
?????? $??0 10 0 0q?6{   $??0q?6?   ??5      ?? 5       ?? 
TargetType" ??6   .?5      ??  ?? Property ? ?? ?? Value
 ???? 63   ?? 5       ? ?? 5      ?? 5       ?? 
TargetType" ??6.   .R?5   "   ?? $??Center=?63   $??Center=?6P   5      5       ?? Triggers 5      P? 5       P? Property ?@J?IsMouseOver P? Value . . ?5      ??  ?@J?RenderTransformOrigin$ 0.5, 0.5&?6F   ?? 5       ? z? 5   "   ?? 5    &    ?? ScaleX$	 1.2Q?65   	 ?? ScaleY$		 1.2Q?6B   5"      ?? 5#       ? ??
 ???? 6B   5$      5&      .??5(      ?? ??6   ?? *
TabImage+5)      ?? 59   
   ???#FFC18E$??1??6B   $??Center=?6O   $??Center=?6s   $??0 0 1 0q?6?   ??6   ?? *
TabLabel+6?   ?? 5:   
   $	??Top=?6   $??0??6)   $??.5q?66   
???   ?6K   ?? 5;   
   $??Bottom=?6   $??1??6,   $??.5q?69   
???   ?6N   5<      5=      