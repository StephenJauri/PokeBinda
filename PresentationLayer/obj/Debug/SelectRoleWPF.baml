   M S B A M L   `   `   `  ????L  HPresentationLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:PresentationLayerPresentationLayer  5   	   %    PresentationLayer.SelectRoleWPF   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:PresentationLayer     5   	     ? Title$  Select Type??5   	   $	??300??6   $	??200??6*    ? 
ResizeMode$ NoResize=?66    ? WindowStartupLocation$ CenterOwner=?6L   ??5	      ?? 5
   
    ?? EndPoint$
 .6,1&?6    ?? 
StartPoint$
 .5,0&?6.   .??5      ?  ? Color$ 	#FF7A4069??6    ? Offset$ 0.091Q?6-   ? 5      $ 	#FFA7809B??6   $ 1Q?6-   5      .??5      ?  ? RowDefinitions 5   
   ?? 5      ?? 5      $??60 ?6   .+?5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ TypeGrid??5   ?   	   HeadersVisibility$	 Column=?6   
   
IsReadOnly
 .    AutoGenerateColumns .     SelectionMode$ Single=?6\      MinColumnWidth$ 40Q?6s      ColumnHeaderStyle 5      ?? 5       ?? 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6   .?5      ??  ?? Property ? ?? ?? Value 5      ?? 5      $ 0.5,1&?62   $ 0.5,0&?6C   .??5   "   ? $ 	#FF513252??6/   $ 0Q?6A   ? 5   "   $ 	#FF6C526D??6/   $ 0.743Q?6A   5      5      ?? 5       ? ?? ?#FFC18E63   ?? 5        ? ??$ 15(?61   5!         RowStyle 5#      ?? 5$      )  #System.Windows.Controls.DataGridRow"  6   .?5%      ??  ? ?? 5&      ?? 5'       ?? Color$ Transparent??6.   5)      ?? 5*       ? ?? ?#FFC18E63   ?? 5+       ? ??$ 15(?61   5,      ??5.      ?? 5/      "  6   .?50      ??  ? ??
 ???? 51      52         Columns 54      0  *System.Windows.Controls.DataGridTextColumn  55      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$	 150 69      Header$
 Role??6E      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   ?? 6S   57      ? 58   
   $??1??6    ? ColumnDefinitions 59      ?? 5:      ?? 5;      .+?5=      ?? -   5         ??Name$ 	btnSelect??5=   ?   $??1??6   $??0??6"   $??40??62   $??40??6>   $??Right=?6I   $??5q?6e   
?????? $??0q?6?   ??5>      ?? 5?      " ??6   .?5@      ??  ? ??
 ???? 67   ?? 5A       ? ?? 5B      ?? 5C   "    ?? 
TargetType" ??62   .R?5D   &   ?? $??Center=?67   $??Center=?6T   5E   #   5G       ?? Triggers 5H      P? 5I       P? Property ?@J?IsMouseOver P? Value . . ?5J   "   ??  ?@J?RenderTransformOrigin$ 0.5, 0.5&?6J   ?? 5K   "    ? z? 5L   &   ?? 5M   *    ?? ScaleX$	 1.2Q?69     ?? ScaleY$	  1.2Q?6F   5O   #   ?? 5P   "    ? ??
 ???? 6F   5Q      5S      .??5U      ?? $(??"images/icons/checkselectedicon.png??6   5V      ?? 5W      -   5        $ 	btnCancel??5W   ?   $??0??6   $??1??6"   $??47??62   $??47??6>   $
??Left=?6I   $??5q?6d   
?????? $??0q?6?   ??5X      ?? 5Y      " ??6   .?5Z      ??  ? ??
 ???? 67   ?? 5[       ? ?? 5\      ?? 5]   "   " ??62   .R?5^   &   ?? $??Center=?67   $??Center=?6T   5_   #   5a       5b      P? 5c       ?@J?IsMouseOver . . ?5d   "   ??  ?@J?RenderTransformOrigin$ 0.5, 0.5&?6J   ?? 5e   "    ? z? 5f   &   ?? 5g   *   $	 1.2Q?69   $	  1.2Q?6F   5i   #   ?? 5j   "    ? ??
 ???? 6F   5k      5m      .??5o      ?? $$??images/icons/xselectedicon.png??6   5p      5q      5r      5s      