   M S B A M L   `   `   `  ????L  HPresentationLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:PresentationLayerPresentationLayer  5      *    $PresentationLayer.ViewAllAbilitiesUC   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:PresentationLayer     5      .??5	      ?   ? ColumnDefinitions  5
   
   ?? 5      ?? 5      $	??100 ?6   .+?5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ AbilityGrid??5   ?      HeadersVisibility$ Column=?6      
IsReadOnly .    AutoGenerateColumns .     SelectionMode$ Single=?6\      MinColumnWidth$ 40Q?6s      ColumnHeaderStyle 5      ?? 5       ?? 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6   .?5      ?? 	 ?? Property	 ? ??
 ?? Value
 5      ?? 5       ?? EndPoint$ 0.5,1&?62    ?? 
StartPoint$ 0.5,0&?6C   .??5   "   ?  ? Color$ 	#FF513252??6/    ? Offset$ 0Q?6A   ? 5   "   $ 	#FF6C526D??6/   $ 0.743Q?6A   5      5      ?? 5      	 ? ??
 ?#FFC18E63   ?? 5      	 ? ??$
 15(?61   5         RowStyle 5      ?? 5      )  #System.Windows.Controls.DataGridRow"  6   .?5      ?? 	 ? ??
 5       ?? 5!       ?? Color$ Transparent??6.   5#      ?? 5$      	 ? ??
 ?#FFC18E63   ?? 5%      	 ? ??$
 15(?61   5&      ??5(      ?? 5)      "  6   .?5*      ?? 	 ? ??
 5+      ?? 5,      $
 .6,1&?62   $
 .5,0&?6B   .??5-   "   ? $ 	#FF7A4069??6/   $ 0.091Q?6A   ? 5.   "   $ 	#FFA7809B??6/   $ 1Q?6A   5/      51      52         Columns 54      0  *System.Windows.Controls.DataGridTextColumn  55      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$	 150 6>      Header$
 Name??6J      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   ?? *Name+   ElementStyle 56      ?? 57      " ??6    .?58      ?? 	 ? ??$

 Wrap=?6=   59      5;        5<      $	 300 6E   $ Description??6Q    .  6%   ?? *Description+ 5=      ?? 5>      " ??6    .?5?      ?? 	 ? ??$

 Wrap=?6=   5@      5B      5D      ? 5E   
   $??1??6    ? RowDefinitions 5F      ?? 5G      $	??100 ?6    ?? 5H      $	??100 ?6    ?? 5I      ?? 5J      $	??100 ?6    .+?5L      ?? -   5         ??Name$ btnAddAbility??5L   b   $??0??6   $??0??6"   $??70??62   $??70??6>   
?????? $??0q?6y   ??5M      ?? 5N      " ??6   .?5O      ?? 	 ? ??

 ???? 67   ?? 5P      	 ? ??
 5Q      ?? 5R   "    ?? 
TargetType" ??62   .R?5S   &   ?? $??Center=?67   $??Center=?6T   5T   #   5V       ?? Triggers 5W      P? 5X       P? Property ?@J?IsMouseOver P? Value . . ?5Y   "   ?? 	 ?@J?RenderTransformOrigin$
 0.5, 0.5&?6J   ?? 5Z   "   	 ? z?
 5[   &   ?? 5\   *    ?? ScaleX$	 1.2Q?69    ?? ScaleY$	 1.2Q?6F   5^   #   ?? 5_   "   	 ? ??

 ???? 6F   5`      5b      .??5d      ?? $??images/icons/plusicon.png??6   5e      ?? 5f      -   5        $ btnEditAbility??5f   b   $??1??6   $??0??6"   $??70??62   $??70??6>   
?????? $??0q?6z   ??5g      ?? 5h      " ??6   .?5i      ?? 	 ? ??

 ???? 67   ?? 5j      	 ? ??
 5k      ?? 5l   "   " ??62   .R?5m   &   ?? $??Center=?67   $??Center=?6T   5n   #   5p       5q      P? 5r       ?@J?IsMouseOver . . ?5s   "   ?? 	 ?@J?RenderTransformOrigin$
 0.5, 0.5&?6J   ?? 5t   "   	 ? z?
 5u   &   ?? 5v   *   $	 1.2Q?69   $	 1.2Q?6F   5x   #   ?? 5y   "   	 ? ??

 ???? 6F   5z      5|      .??5~      ?? $??images/icons/penicon.png??6   5      ?? 5?      -   5        $ btnSelectAbility??5?   v   $y?Hidden=?6   $??3??6)   $??0??66   $??70??6F   $??70??6R   
?????? $??0q?6?   ??5?      ?? 5?      " ??6   .?5?      ?? 	 ? ??

 ???? 67   ?? 5?      	 ? ??
 5?      ?? 5?   "   " ??62   .R?5?   &   ?? $??Center=?67   $??Center=?6T   5?   #   5?       5?      P? 5?       ?@J?IsMouseOver . . ?5?   "   ?? 	 ?@J?RenderTransformOrigin$
 0.5, 0.5&?6J   ?? 5?   "   	 ? z?
 5?   &   ?? 5?   *   $	 1.2Q?69   $	 1.2Q?6F   5?   #   ?? 5?   "   	 ? ??

 ???? 6F   5?      5?      .??5?      ?? -   5         ??Name$ 
imgAddCard??5?      $(??"images/icons/checkselectedicon.png??6,   5?      5?      5?      5?      