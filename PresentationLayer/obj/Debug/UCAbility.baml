   M S B A M L   `   `   `  ????L  HPresentationLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:PresentationLayerPresentationLayer  5      !    PresentationLayer.UCAbility   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:PresentationLayer     5      .??5      ? -   5          ?Name$  abilityGrid??5       ? ColumnDefinitions 5	   
   ?? 5
      ?? 5      $??0 ?6    ? RowDefinitions 5   
   ?? 5      ?? 5      .+?5   
   ?? -   5        $??txtBlockAbilityName??5   ?   $??0??6   ???#FFC18E$??Center=?66   $??Center=?6S   $??5 10 5 0q?6n   $??15(?6?   $
??Wrap=?6?   Ability Name6?   ?? 5   
   -   5        $ ??txtBlockAbilityDescription??5   ?   $??1??6   ???#FFC18E$??Center=?66   $??Center=?6S   $
??5 10q?6n   $??15(?6|   $
??Wrap=?6?   ??This is the extra lengthy description of the ability at its full length as show on the card, this can reach up to 250 characters long6G  ?? 5   
   -   5         ??Name$ btnRemoveAbility??5   ?   $y?	Collapsed=?6   $??0??6(   $??1??65   $??2??6E   $??40??6V   $??40??6b   $??Center=?6m   $??Center=?6?   $??5q?6?   
?????? ??5      ?? 5       ?? 
TargetType" ??6   .?5      ??  ?? Property ? ?? ?? Value
 ???? 63   ?? 5       ? ?? 5      ?? 5       ?? 
TargetType" ??6.   .R?5   "   ?? $??Center=?63   $??Center=?6P   5      5       ?? Triggers 5      P? 5       	 P? Property	 ?@J?IsMouseOver
 P? Value
 . . ?5!      ??  ?@J?RenderTransformOrigin$ 0.5, 0.5&?6F   ?? 5"       ? z? 5#   "   ?? 5$   &    ?? ScaleX$	 1.2Q?65    ?? ScaleY$	 1.2Q?6B   5&      ?? 5'       ? ??
 ???? 6B   5(      5*      .??5,      ?? $ ??images/icons/minusicon.png??6   5-      5.      5/      