   M S B A M L   `   `   `  ????L  HPresentationLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:PresentationLayerPresentationLayer  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   &     PresentationLayer.ViewProfileWPF   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:PresentationLayer     5   	   9System&clr-namespace:System;assembly=mscorlib     5   	     ? Title$  View Profile??5	   	    ? SizeToContent$ Height=?6   $	??560??65    ? 
ResizeMode$ NoResize=?6A    ? WindowStartupLocation$ CenterOwner=?6W   ??5
      ?? 5   
    ?? EndPoint$
 .6,1&?6    ?? 
StartPoint$
 .5,0&?6.   .??5      ?  ? Color$ 	#FF7A4069??6    ? Offset$ 0.091Q?6-   ? 5      $ 	#FFA7809B??6   $ 1Q?6-   5      .??5      ?  ? RowDefinitions 5   
   ?? 5      ?? 5      $??80 ?6    .+?5   
   ? $??0??6    5      ?? 5      $	??100 ?6    ?? 5      $??50 ?6    ?? 5      $??50 ?6    ?? 5      $??50 ?6    ?? 5      $??50 ?6    ?? 5      $??50 ?6    	 ? ColumnDefinitions	 5      ?? 5       $	??187 ?6#   ?? 5!      .+?5#      ?? -   5        
 ??Name$
 btnEditProfile??5#   ?   $??0??6   $??1??6"   $??40??62   $??40??6>   $	??Top=?6I   $??Right=?6a   $??10q?6}   
?????? ??5$      ?? 5%       ?? 
TargetType" ??6   .?5&      ??  ?? Property ? ?? ?? Value
 ???? 67   ?? 5'       ? ?? 5(      ?? 5)   "    ?? 
TargetType" ??62   .R?5*   &   ?? $??Center=?67   $??Center=?6T   5+   #   5-       ?? Triggers 5.      P? 5/       P? Property ?@J?IsMouseOver P? Value . . ?50   "   ??  ?@J?RenderTransformOrigin$ 0.5, 0.5&?6J   ?? 51   "    ? z? 52   &   ?? 53   *    ?? ScaleX$	 1.2Q?69    ?? ScaleY$	 1.2Q?6F   55   #   ?? 56   "    ? ??
 ???? 6F   57      59      .??5;      ?? -   5         ??Name$ imgEditStatus??5;      $??images/icons/penicon.png??6/   5<      ?? 5=      -   5         ??Name$ txtBlockWelcome??5=   ?   $??Center=?6   $??Center=?63   $??5q?6P   $??0??6[   $??0??6k   $??2??6x   ???#FFC18E$??28(?6?   Hello Your Name6?   ?? 5>      $??Center=?6   $??Right=?63   $??5 0 5 15q?6P   $??0??6c   $??1??6s   ???#FFC18E$??20(?6?   
Given Name6?   ?? 5?      $??Center=?6   $??Right=?63   $??5 0 5 15q?6P   $??0??6c   $??2??6s   ???#FFC18E$??20(?6?   Family Name6?   ?? 5@      $??Center=?6   $??Right=?63   $??5 0 5 15q?6P   $??0??6c   $??3??6s   ???#FFC18E$??20(?6?   Email6?   ?? 5A      $??Center=?6   $??Right=?63   $??5 0 5 15q?6P   $??0??6c   $??4??6s   ???#FFC18E$??20(?6?   
Birthday6?   ?? 5B      $??Center=?6   $??Right=?63   $??5 0 5 15q?6P   $??0??6c   $??5??6s   ???#FFC18E$??20(?6?   
Password6?   ?? 5D      -   5         ??Name$ txtBoxGivenName??5D   '  $??1??6   $??1??6#   $??	5 0 50 15q?63   $??30??6F   $??Center=?6R   $??Center=?6t   $??15(?6?   
???   ?$
??Left=?6?   ???#FFC18E$??0q?6?   
??????  ?? 
IsReadOnly . .??6@  
Given Name6L  ?? 5E      -   5        $ txtBoxFamilyName??5E   '  $??2??6   $??1??6#   $??	5 0 50 15q?63   $??30??6F   $??Center=?6R   $??Center=?6t   $??15(?6?   
???   ?$
??Left=?6?   ???#FFC18E$??0q?6?   
??????  . .??6A  Family Name6N  ?? 5F      -   5        $ txtBoxEmail??5F   '  $??3??6   $??1??6#   $??	5 0 50 15q?63   $??30??6F   $??Center=?6R   $??Center=?6t   $??15(?6?   
???   ?$
??Left=?6?   ???#FFC18E$??0q?6?   
??????  . .??6<  emai******@gmail.com6R  ?? 5G      -   5        $ txtBoxBirthday??5G   '  $??4??6   $??1??6#   $??	5 0 50 15q?63   $??30??6F   $??Center=?6R   $??Center=?6t   $??15(?6?   
???   ?$
??Left=?6?   ???#FFC18E$??0q?6?   
??????  . .??6?  September 27, 19986S  ?? 5H      -   5        $ txtBoxPassword??5H   '  $??5??6   $??1??6#   $??	5 0 50 15q?63   $??30??6F   $??Center=?6R   $??Center=?6t   $??15(?6?   
???   ?$
??Left=?6?   ???#FFC18E$??0q?6?   
??????  . .??6?  	*********6J  ?? 5K      -	   5        $
 btnChangePassword??5K   ?   $??5??6   $??1??6"   $??25??62   $??25??6>   $	??Top=?6I   $??Right=?6a   $??0 5 55 0q?6}   $y?Hidden=?6?   
?????? ??5L      ?? 5M      " ??6   .?5N      ??  ? ??
 ???? 67   ?? 5O       ? ?? 5P      ?? 5Q   "   " ??62   .R?5R   &   ?? $??Center=?67   $??Center=?6T   5S   #   5U       5V      P? 5W       ?@J?IsMouseOver . . ?5X   "   ??  ?@J?RenderTransformOrigin$ 0.5, 0.5&?6J   ?? 5Y   "    ? z? 5Z   &   ?? 5[   *   $	 1.2Q?69   $	 1.2Q?6F   5]   #   ?? 5^   "    ? ??
 ???? 6F   5_      5a      .??5c      ?? $??images/icons/penicon.png??6   5d      ?? 5f      -
   5         ??Name$ lblValidationGivenName??5f   ?   $??1??6   $??1??6!   $??10 0 0 0q?61   $??Bottom=?6C   $??10(?6^   $
??Left=?6l   
???????$y?Hidden=?6?   .??6?   
Given Name6?   ?? 5g      -   5        $ lblValidationFamilyName??5g   ?   $??2??6   $??1??6!   $??10 0 0 0q?61   $??Bottom=?6C   $??10(?6^   $
??Left=?6l   
???????$y?Hidden=?6?   .??6?   Family Name6?   (  "System.Windows.Controls.DatePicker  5i      -   5          Name$ datePickerBirthday??5i      $y?Hidden=?65   $??1??6I   $??4??6Y   $??Right=?6f   $??25??6?   $??25??6?   $??Center=?6?   $??
0 -15 50 0q?6     DisplayDateEnd 6?   ??  ?? Mode$ OneWay=? ?? Today ?? Source# Z ??5j      ?? 5k      "  6   .?5l      ??  ?@?LayoutTransform 5m      ?? 5n   "   $	 1.5Q?61   $	 1.5Q?6>   5p      ?? 5q       ? ??
 ???? 67   ?? 5r       ? ??$ 0q?6<   ?? 5s       ? ??$ 0q?63    ?? 	Resources 5t      :  4System.Windows.Controls.Primitives.DatePickerTextBox??%V   '        ?? 5u      "  6$   .?5v   "   ??  ? y?$ Hidden=?6?   5w      5x      5y      5{      5}      ? 5~   
   $??1??6   	 5      ?? 5?      $??80 ?6#   ?? 5?      $??80 ?6#   ?? 5?      ?? 5?      $??80 ?6#   .+?5?      ?? -   5        $
 btnSave??5?   t   {?.  $??0??6'   $??0??64   $??70??6D   $??70??6P   
?????? $y?Hidden=?6?   $??0q?6?   ??5?      ?? 5?      " ??6   .?5?      ??  ? ??
 ???? 67   ?? 5?       ? ?? 5?      ?? 5?   "   " ??62   .R?5?   &   ?? $??Center=?67   $??Center=?6T   5?   #   5?       5?      P? 5?       ?@J?IsMouseOver . . ?5?   "   ??  ?@J?RenderTransformOrigin$ 0.5, 0.5&?6J   ?? 5?   "    ? z? 5?   &   ?? 5?   *   $	 1.2Q?69   $	 1.2Q?6F   5?   #   ?? 5?   "    ? ??
 ???? 6F   5?      5?      .??5?      ?? $??images/icons/saveicon.png??6   5?      ?? 5?      -   5        $
 	btnCancel??5?   ?   {?.  $??0??6'   $y?Hidden=?64   $??1??6H   $??70??6X   $??70??6d   
?????? $??0q?6?   ??5?      ?? 5?      " ??6   .?5?      ??  ? ??
 ???? 67   ?? 5?       ? ?? 5?      ?? 5?   "   " ??62   .R?5?   &   ?? $??Center=?67   $??Center=?6T   5?   #   5?       5?      P? 5?       ?@J?IsMouseOver . . ?5?   "   ??  ?@J?RenderTransformOrigin$ 0.5, 0.5&?6J   ?? 5?   "    ? z? 5?   &   ?? 5?   *   $	 1.2Q?69   $	 1.2Q?6F   5?   #   ?? 5?   "    ? ??
 ???? 6F   5?      5?      .??5?      ?? $!??images/icons/cancelicon.png??6   5?      ?? 5?      -   5        $
 btnDeleteAccount??5?   ?   {?.  $??0??6'   $??3??64   $y?Hidden=?6D   $??30??6X   $??30??6d   
?????? $??10q?6?   $??Right=?6?   $??Bottom=?6?   $??0q?6?   ??5?      ?? 5?      " ??6   .?5?      ??  ? ??
 ???? 67   ?? 5?       ? ?? 5?      ?? 5?   "   " ??62   .R?5?   &   ?? $??Center=?67   $??Center=?6T   5?   #   5?       5?      P? 5?       ?@J?IsMouseOver . . ?5?   "   ??  ?@J?RenderTransformOrigin$ 0.5, 0.5&?6J   ?? 5?   "    ? z? 5?   &   ?? 5?   *   $	 1.2Q?69   $	 1.2Q?6F   5?   #   ?? 5?   "    ? ??
 ???? 6F   5?      5?      .??5?      ?? -   5        $ imgDeleteAccount??5?      $ ??images/icons/trashicon.png??62   5?      5?      5?      5?      