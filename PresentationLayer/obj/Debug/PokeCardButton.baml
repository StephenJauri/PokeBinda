   M S B A M L   `   `   `  ????L  HPresentationLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:PresentationLayerPresentationLayer  5      &     PresentationLayer.PokeCardButton   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:PresentationLayer     5      .??5	      (?   (? StretchDirection$
  Both=?6    (? Stretch$ Uniform=?6'   .??5
   
   ? $	??8 5q?6    ? RowDefinitions 5      ?? 5      $??95 ?6    ?? 5      .+?5      ?? ???#FFC18E$??1??6K   $
??Wrap=?6X   $??7(?6l   $??20??6y   $??63??6?   $??Center=?6?    ?? TextAlignment$ Center=?6?   $??Center=?6?   ??6   ?? *
CardText+6?   ?? 5      -   5        $??
ButtonMask??5      
?????? $??0??6@   $??0??6M   ??5      ?? 5       ?? 
TargetType" ??6   .?5      ??  ?? Property ? ?? ?? Value
 ???? 67   ?? 5       ? ?? 5      ?? 5   "    ?? 
TargetType" ??62   .R?5   &   ?? $??Center=?67   $??Center=?6T   5   #   5       ?? Triggers 5      P? 5      	 P? Property	 ?@J?IsMouseOver
 P? Value
 . . ?5   "   ??  ?@J?RenderTransformOrigin$ 0.5, 0.5&?6J   ?? 5   "    ? z? 5   &   ?? 5    *    ?? ScaleX$	 1.1Q?69    ?? ScaleY$
 1.11Q?6F   5"   #   ?? 5#   "    ? ??
 ???? 6F   5$      5&      .??5(      ?? -   5        $??	CardImage??5(      $??88??6)   $??63??65   $??0??6@   $	??5 2q?6M   5)      5*      5+      5,      