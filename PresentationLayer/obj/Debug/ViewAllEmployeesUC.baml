   M S B A M L   `   `   `  ????L  HPresentationLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:PresentationLayerPresentationLayer  5      *    $PresentationLayer.ViewAllEmployeesUC   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:PresentationLayer     5      .??5	      ?   ? ColumnDefinitions  5
   
   ?? 5      ?? 5      $	??100 ?6   .+?5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ EmployeeGrid??5   ?      HeadersVisibility$ Column=?6      
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
 .5,0&?6B   .??5-   "   ? $ 	#FF7A4069??6/   $ 0.091Q?6A   ? 5.   "   $ 	#FFA7809B??6/   $ 1Q?6A   5/      51      52         Columns 54      0  *System.Windows.Controls.DataGridTextColumn  55      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 6<      Header$ ID??6G      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   ?? *ID+6S     56      $	 150 6?   $ Email??6K    .  6%   ?? *Email+6Z     57      $	 100 6C   $ 
Given Name??6O    .  6%   ?? *	GivenName+6c     58      $	 100 6D   $ Family Name??6P    .  6%   ?? *
FamilyName+6e     59      $ 75 6B   $ Birthday??6M    .  6%   ?? *
Birthday+6_     5:      $ 75 6F   $ Created??6Q    .  6%   ?? *CreationDate+6b     5;      $	 100 6E   $ Roles??6Q    .  6%   ?? *RolesString+6`     5<      $ 50 6@   $ Active??6K    .  6%   ?? *Active+6[   5>      ? 5?   
   $??1??6    ? RowDefinitions 5@      ?? 5A      $	??100 ?6    ?? 5B      $	??100 ?6    ?? 5C      ?? 5D      $	??100 ?6    .+?5F      ?? -   5         ??Name$ btnAdd??5F   b   $??0??6   $??0??6"   $??70??62   $??70??6>   
?????? $??0q?6r   ??5G      ?? 5H      " ??6   .?5I      ?? 	 ? ??

 ???? 67   ?? 5J      	 ? ??
 5K      ?? 5L   "    ?? 
TargetType" ??62   .R?5M   &   ?? $??Center=?67   $??Center=?6T   5N   #   5P       ?? Triggers 5Q      P? 5R       P? Property ?@J?IsMouseOver P? Value . . ?5S   "   ?? 	 ?@J?RenderTransformOrigin$
 0.5, 0.5&?6J   ?? 5T   "   	 ? z?
 5U   &   ?? 5V   *    ?? ScaleX$	 1.2Q?69    ?? ScaleY$	 1.2Q?6F   5X   #   ?? 5Y   "   	 ? ??

 ???? 6F   5Z      5\      .??5^      ?? $??images/icons/plusicon.png??6   5_      ?? 5`      -   5        $ btnEdit??5`   b   $??1??6   $??0??6"   $??70??62   $??70??6>   
?????? $??0q?6s   ??5a      ?? 5b      " ??6   .?5c      ?? 	 ? ??

 ???? 67   ?? 5d      	 ? ??
 5e      ?? 5f   "   " ??62   .R?5g   &   ?? $??Center=?67   $??Center=?6T   5h   #   5j       5k      P? 5l       ?@J?IsMouseOver . . ?5m   "   ?? 	 ?@J?RenderTransformOrigin$
 0.5, 0.5&?6J   ?? 5n   "   	 ? z?
 5o   &   ?? 5p   *   $	 1.2Q?69   $	 1.2Q?6F   5r   #   ?? 5s   "   	 ? ??

 ???? 6F   5t      5v      .??5x      ?? $??images/icons/penicon.png??6   5y      5z      5{      5|      