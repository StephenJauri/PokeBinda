   M S B A M L   `   `   `  ????L  HPresentationLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:PresentationLayerPresentationLayer  5      (    "PresentationLayer.TextWithRemoveUC   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:PresentationLayer     5      .??5      ? -   5          ?Name$  abilityGrid??5       ? ColumnDefinitions 5	   
   ?? 5
      ?? 5      $??50 ?6   .+?5   
   ?? -   5        $??txtBlockItem??5   ?   ???#FFC18E$??Center=?6)   $??Center=?6F   $??5 0 5 0q?6a   $??15(?6r   $
??Wrap=?6?   	Text Item6?   ?? 5   
   -   5         ??Name$ btnRemoveItem??5   ?   $y?Visible=?6   $??0??6&   $??1??63   $??40??6C   $??40??6O   $??Center=?6Z   $??Center=?6u   $??5q?6?   
?????? ??5      ?? 5       ?? 
TargetType" ??6   .?5      ??  ?? Property ? ?? ?? Value
 ???? 63   ?? 5       ? ?? 5      ?? 5       ?? 
TargetType" ??6.   .R?5   "   ?? $??Center=?63   $??Center=?6P   5      5       ?? Triggers 5      P? 5       P? Property ?@J?IsMouseOver	 P? Value	 . . ?5      ??  ?@J?RenderTransformOrigin$ 0.5, 0.5&?6F   ?? 5       ? z? 5   "   ?? 5   &   
 ?? ScaleX$	
 1.2Q?65    ?? ScaleY$	 1.2Q?6B   5!      ?? 5"       ? ??
 ???? 6B   5#      5%      .??5'      ?? $ ??images/icons/minusicon.png??6   5(      5)      5*      