   M S B A M L   `   `   `  ????L  HPresentationLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:PresentationLayerPresentationLayer  5      !    PresentationLayer.UCPokemon   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:PresentationLayer     5      .??5	      ? -   5          ?Name$  mainGrid??5	       ? ColumnDefinitions 5
   
   ?? 5      $??80 ?6   ?? 5      $	??100 ?6   ?? 5      ?? 5      $??0 ?6   .+?5   
   ?? -   5        $??txtBlockGen??5   ?   $??0??6   $??Center=?6$   $??Center=?6?   $??5q?6\   $??20(?6g   ???#FFC18EGen6?   ?? 5   
   -   5        $??txtBlockPokedex??5   ?   $??1??6   $??Center=?6$   $??Center=?6?   $??5q?6\   $??20(?6g   ???#FFC18E	Pokedex #6?   ?? 5   
   -   5        $??txtBlockName??5   ?   $??2??6   $??Center=?6$   $??Center=?6?   $??5q?6\   $??20(?6g   ???#FFC18EName6?   ?? 5   
   -   5         ??Name$ btnRemovePokemon??5   ?   $y?	Collapsed=?6   $??0??6(   $??3??65   $??40??6E   $??40??6Q   $??Center=?6\   $??Center=?6w   $??5q?6?   
?????? ??5      ?? 5       ?? 
TargetType" ??6   .?5      ??  ?? Property ? ?? ?? Value
 ???? 63   ?? 5       ? ?? 5      ?? 5       ?? 
TargetType" ??6.   .R?5   "   ?? $??Center=?63   $??Center=?6P   5      5       ?? Triggers 5      P? 5        P? Property ?@J?IsMouseOver	 P? Value	 . . ?5!      ??  ?@J?RenderTransformOrigin$ 0.5, 0.5&?6F   ?? 5"       ? z? 5#   "   ?? 5$   &   
 ?? ScaleX$	
 1.2Q?65    ?? ScaleY$	 1.2Q?6B   5&      ?? 5'       ? ??
 ???? 6B   5(      5*      .??5,      ?? $ ??images/icons/minusicon.png??6   5-      5.      5/      