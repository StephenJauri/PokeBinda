   M S B A M L   `   `   `  ????L  HPresentationLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:PresentationLayerPresentationLayer  5   	   ,    &PresentationLayer.CreateAndEditCardWPF   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:PresentationLayer     5   	     ? Title$  	Edit Card??5   	   $	??750??6   $	??900??6(    ? WindowStartupLocation$ CenterOwner=?64   ??5	      ?? 5
   
    ?? EndPoint$
 .6,1&?6    ?? 
StartPoint$
 .5,0&?6.   .??5      ?  ? Color$ 	#FF7A4069??6    ? Offset$ 0.091Q?6-   ? 5      $ 	#FFA7809B??6   $ 1Q?6-   5      .??5      ?  ? RowDefinitions 5   
   ?? 5      ?? 5      $	??100 ?6    ? ColumnDefinitions 5   
   ?? 5      ?? 5      ?? 5      .+?5   
   ? $??0??6    5      ?? 5      $	??120 ?6    ?? 5      ?? 5      $??70 ?6    .+?5      ?? -   5         ??Name$ btnEditImage??5   b   $??2??6   $??0??6"   $??60??62   $??60??6>   
?????? $??0q?6x   $??0 0 0 5q?6?   ??5       ?? 5!      	 ?? 
TargetType"	 ??6   .?5"      ?? 
 ?? Property
 ? ?? ?? Value
 ???? 67   ?? 5#      
 ? ?? 5$      ?? 5%   "    ?? 
TargetType" ??62   .R?5&   &   ?? $??Center=?67   $??Center=?6T   5'   #   5)       ?? Triggers 5*      P? 5+       P? Property ?@J?IsMouseOver P? Value . . ?5,   "   ?? 
 ?@J?RenderTransformOrigin$ 0.5, 0.5&?6J   ?? 5-   "   
 ? z? 5.   &   ?? 5/   *    ?? ScaleX$	 1.2Q?69    ?? ScaleY$	 1.2Q?6F   51   #   ?? 52   "   
 ? ??
 ???? 6F   53      55      .??57      ?? $??images/icons/penicon.png??6   58      ?? 59      -   5         ??Name$ txtBoxCardName??59   
  $??Center=?6   $	??260??61   $??Center=?6=   
???   ?
?????? $??Center=?6?    ?? TextWrapping$
 Wrap=?6?   ???#FFC18E$??5q?6?   $??20(?6?   $??0??6?   $??0??6?   .??6"  	Card Name6-  ?? 5:      -   5         ??Name$ imgCardImage??5:   =   $??0??6   $??1??6$   $??10q?61   $??images/cards/default.png??6S   5;      ?? 5<   
   $??1??6   $??2??6'   $
??Auto=?6;   $??Disabled=?6^   .??5=      ? $??1??6    5>      ?? 5?      $??60 ?6$   ?? 5@      ?? 5A      $??60 ?6$   ?? 5B      ?? 5C      ?? 5D      ?? 5E      $??60 ?6$   ?? 5F      $??60 ?6$   ?? 5G      $??60 ?6$   ?? 5H      $??60 ?6$    5J      ?? 5K      $	??150 ?6'   ?? 5L      ?? 5M      $??60 ?6'   .+?5O      ?? 
???   ?$??0??6-   $??1q?6=   $??Right=?6Q   $??10??6m   ?? 5P      
???   ?$??1??6-   $??1q?6=   $??Right=?6Q   $??10??6m   ?? 5Q      
???   ?$??1q?6-   $??3??6A   $??Bottom=?6U   $??0??6p   ?? 5R      
???   ?$??1q?6-   $??3??6A   $??Bottom=?6U   $??1??6p   ?? 5S      
???   ?$??1q?6-   $??3??6A   $??Bottom=?6U   $??2??6p   ?? 5T      
???   ?$??1q?6-   $??3??6A   $??Bottom=?6U   $??3??6p   ?? 5U      
???   ?$??1q?6-   $??3??6A   $??Bottom=?6U   $??4??6p   ?? 5V      
???   ?$??1q?6-   $??3??6A   $??Bottom=?6U   $??5??6p   ?? 5W      
???   ?$??1q?6-   $??3??6A   $??Bottom=?6U   $??6??6p   ?? 5X      
???   ?$??1q?6-   $??3??6A   $??Bottom=?6U   $??7??6p   ?? 5Y      
???   ?$??1q?6-   $??3??6A   $??Bottom=?6U   $??8??6p   ?? 5Z      -   5         ??Name$ txtBlockNotes??5Z   ?   $??Center=?6   $??Right=?67   ???#FFC18E$??5q?6h   $??20(?6s   $??0??6?   $??0??6?   Notes6?   ?? 5[      -   5        $ txtBlockTags??5[   ?   $??Center=?6   $??Right=?67   ???#FFC18E$??5q?6h   $??20(?6s   $??1??6?   $??0??6?   Tag6?   ?? 5\      -   5        $ 
txtBlockHP??5\   ?   $??Center=?6   $??Right=?67   ???#FFC18E$??5q?6h   $??20(?6s   $??2??6?   $??0??6?   HP6?   ?? 5]      -   5        $ txtBlockPokemon??5]   ?   $??Center=?6   $??Right=?67   ???#FFC18E$??5q?6h   $??20(?6s   $??3??6?   $??0??6?   	Pokemon6?   ?? 5^      -   5        $ txtBlockElement??5^   ?   $??Center=?6   $??Right=?67   ???#FFC18E$??5q?6h   $??20(?6s   $??4??6?   $??0??6?   Type6?   ?? 5_      -	   5        $ txtBlockAbility??5_   ?   $??Center=?6   $??Right=?67   ???#FFC18E$??5q?6h   $??20(?6s   $??5??6?   $??0??6?   	Ability6?   ?? 5`      -
   5        $ txtBlockSetNumber??5`   ?   $??Center=?6   $??Right=?67   ???#FFC18E$??5q?6h   $??20(?6s   $??6??6?   $??0??6?   
Set Number6?   ?? 5a      -   5        $ txtBlockReleaseDate??5a   ?   $??Center=?6   $??Right=?67   ???#FFC18E$??5q?6h   $??20(?6s   $??7??6?   $??0??6?   Release Date6?   ?? 5b      -   5        $ txtBlockReleased??5b   ?   $??Center=?6   $??Right=?67   ???#FFC18E$??5q?6h   $??20(?6s   $??8??6?   $??0??6?   	Visible6?   ?? 5c      -   5        $ txtBlockActive??5c   ?   $??Center=?6   $??Right=?67   ???#FFC18E$??5q?6h   $??20(?6s   $??9??6?   $??0??6?   Active6?   ?? 5e      -   5        $ btnEditTags??5e   ?   $??1??6   $??2??6&   $??40??66   $??40??6B   $??Center=?6M   $??Center=?6h   $??10q?6?   
?????? ??5f      ?? 5g      "	 ??6    .?5h      ?? 
 ? ??
 ???? 6;   ?? 5i      
 ? ?? 5j   "   ?? 5k   &   " ??66   .R?5l   *   ?? $??Center=?6;   $??Center=?6X   5m   '   5o       5p      P? 5q   "    ?@J?IsMouseOver . . ?5r   &   ?? 
 ?@J?RenderTransformOrigin$ 0.5, 0.5&?6N   ?? 5s   &   
 ? z? 5t   *   ?? 5u   .   $	 1.2Q?6=   $	 1.2Q?6J   5w   '   ?? 5x   &   
 ? ??
 ???? 6J   5y   #   5{      .??5}      ?? $??images/icons/plusicon.png??6   5~      ?? 5?      -   5        $ btnAddPokemon??5?   ?   $??3??6   $??2??6&   $??40??66   $??40??6B   $??Center=?6M   $??Center=?6h   $??10q?6?   
?????? ??5?      ?? 5?      "	 ??6    .?5?      ?? 
 ? ??
 ???? 6;   ?? 5?      
 ? ?? 5?   "   ?? 5?   &   " ??66   .R?5?   *   ?? $??Center=?6;   $??Center=?6X   5?   '   5?       5?      P? 5?   "    ?@J?IsMouseOver . . ?5?   &   ?? 
 ?@J?RenderTransformOrigin$ 0.5, 0.5&?6N   ?? 5?   &   
 ? z? 5?   *   ?? 5?   .   $	 1.2Q?6=   $	 1.2Q?6J   5?   '   ?? 5?   &   
 ? ??
 ???? 6J   5?   #   5?      .??5?      ?? $??images/icons/plusicon.png??6   5?      ?? 5?      -   5        $ btnEditTypes??5?   ?   $??4??6   $??2??6&   $??40??66   $??40??6B   $??Center=?6M   $??Center=?6h   $??10q?6?   
?????? ??5?      ?? 5?      "	 ??6    .?5?      ?? 
 ? ??
 ???? 6;   ?? 5?      
 ? ?? 5?   "   ?? 5?   &   " ??66   .R?5?   *   ?? $??Center=?6;   $??Center=?6X   5?   '   5?       5?      P? 5?   "    ?@J?IsMouseOver . . ?5?   &   ?? 
 ?@J?RenderTransformOrigin$ 0.5, 0.5&?6N   ?? 5?   &   
 ? z? 5?   *   ?? 5?   .   $	 1.2Q?6=   $	 1.2Q?6J   5?   '   ?? 5?   &   
 ? ??
 ???? 6J   5?   #   5?      .??5?      ?? $??images/icons/plusicon.png??6   5?      ?? 5?      -   5        $ btnAddAbility??5?   ?   $??5??6   $??2??6&   $??40??66   $??40??6B   $??Center=?6M   $??Center=?6h   $??10q?6?   
?????? ??5?      ?? 5?      "	 ??6    .?5?      ?? 
 ? ??
 ???? 6;   ?? 5?      
 ? ?? 5?   "   ?? 5?   &   " ??66   .R?5?   *   ?? $??Center=?6;   $??Center=?6X   5?   '   5?       5?      P? 5?   "    ?@J?IsMouseOver . . ?5?   &   ?? 
 ?@J?RenderTransformOrigin$ 0.5, 0.5&?6N   ?? 5?   &   
 ? z? 5?   *   ?? 5?   .   $	 1.2Q?6=   $	 1.2Q?6J   5?   '   ?? 5?   &   
 ? ??
 ???? 6J   5?   #   5?      .??5?      ?? $??images/icons/plusicon.png??6   5?      ?? 5?      -   5        $ btnChangeReleased??5?   ?   $??8??6   $??2??6&   $??40??66   $??40??6B   $??Center=?6M   $??Center=?6h   $??10q?6?   
?????? ??5?      ?? 5?      "	 ??6    .?5?      ?? 
 ? ??
 ???? 6;   ?? 5?      
 ? ?? 5?   "   ?? 5?   &   " ??66   .R?5?   *   ?? $??Center=?6;   $??Center=?6X   5?   '   5?       5?      P? 5?   "    ?@J?IsMouseOver . . ?5?   &   ?? 
 ?@J?RenderTransformOrigin$ 0.5, 0.5&?6N   ?? 5?   &   
 ? z? 5?   *   ?? 5?   .   $	 1.2Q?6=   $	 1.2Q?6J   5?   '   ?? 5?   &   
 ? ??
 ???? 6J   5?   #   5?      .??5?      ?? $??images/icons/swapicon.png??6   5?      ?? 5?      -   5        $ btnChangeActive??5?   ?   $??9??6   $??2??6&   $??40??66   $??40??6B   $??Center=?6M   $??Center=?6h   $??10q?6?   
?????? ??5?      ?? 5?      "	 ??6    .?5?      ?? 
 ? ??
 ???? 6;   ?? 5?      
 ? ?? 5?   "   ?? 5?   &   " ??66   .R?5?   *   ?? $??Center=?6;   $??Center=?6X   5?   '   5?       5?      P? 5?   "    ?@J?IsMouseOver . . ?5?   &   ?? 
 ?@J?RenderTransformOrigin$ 0.5, 0.5&?6N   ?? 5?   &   
 ? z? 5?   *   ?? 5?   .   $	 1.2Q?6=   $	 1.2Q?6J   5?   '   ?? 5   &   
 ? ??
 ???? 6J   5  #   5     .??5     ?? $??images/icons/swapicon.png??6   5     ?? 5
     -   5        $ tbNotes??5
  ?   
?????? $	??200??63   $
 Wrap=?6B   
???   ?$??Center=?6j   $
??Left=?6?   $??5q?6?   $??1??6?   $??0??6?   ???#FFC18E$??15(?6?   .??6?   
Pristine6  ? 5     -   5         ?Name$ grdTags??5  4   $??1??6   $??1??6$   5     ? 5     -   5        $ grdTypes??5  4   $??4??6   $??1??6$   5     ?? 5     -   5        $ txtBoxHP??5  ?   
?????? $	??200??63   $
 Wrap=?6B   
???   ?$??Center=?6j   $
??Left=?6?   $??5q?6?   $??1??6?   $??2??6?   ???#FFC18E$??15(?6?   6N  ? 5     -   5        $ 
grdPokemon??5  4   $??3??6   $??1??6$    5     ?? 5     $??40 ?6(   .+?5     !   PresentationLayer.UCPokemon  -   5          Name$ 
pokeHeader??5  &   $??0??6:   5     ? 5     -   5        $ grdAbilities??5  3   $??5??6   $??1??6#   5     ?? 5      -   5        $ txtBoxSetNumber??5   ?   
?????? $	??100??63   
???   ?$??Center=?6V   $
??Left=?6q   $??5q?6?   $??1??6?   $??6??6?   ???#FFC18E$??15(?6?   .??6?   	xxx/xxx6?   ?? 5!     -   5        $  txtBlockReleaseDateDetails??5!  ?   $??Center=?6   $
??Left=?67   $??5q?6R   $??1??6]   $??7??6m   ???#FFC18E$??15(?6?   
MM/dd/yyyy6?   (  "System.Windows.Controls.DatePicker  5#     -   5          Name$ datePickerReleaseDate??5#     $??2??6<   $??7??6L   $??Center=?6Y   $??Center=?6v   $??30??6?   $??30??6?   $??Center=?6?   $??5q?6?   ??5$     ?? 5%     "	  6    .?5&     ?? 
 ?@?LayoutTransform 5'  "   ?? 5(  &   $	 1.5Q?65   $	 1.5Q?6B   5*     ?? 5+     
 ? ??
 ???? 6;   ?? 5,     
 ? ??$ 0q?6@   ?? 5-     
 ? ??$ 0q?67    ?? 	Resources 5.     :  4System.Windows.Controls.Primitives.DatePickerTextBox??%Y   '        ?? 5/  "   "	  6(   .?50  &   ?? 
 ? y?$ 	Collapsed=?6C   51  #   52     53     55     ?? 57     -   5        $ imgReleased??57     $$??images/icons/xselectedicon.png??6-   $??1??6U   $??8??6e   $
??Left=?6r   $??40??6?   $??40??6?   $??5q?6?   ?? 58     -   5        $ 	imgActive??58     $(??"images/icons/checkselectedicon.png??6+   $??1??6W   $??9??6g   $
??Left=?6t   $??40??6?   $??40??6?   $??5q?6?   59     5:     ? 5<  
   $??1??6   $??0??6   $??3??6,    5=     ?? 5>     ?? 5?     $	??100 ?6#   ?? 5@     $	??100 ?6#   .+?5C     ?? -    5        $ 	btnCancel??5C  b   $??0??6   $??2??6"   $??80??62   $??80??6>   
?????? $??0q?6u   ??5D     ?? 5E     "	 ??6   .?5F     ?? 
 ? ??
 ???? 67   ?? 5G     
 ? ?? 5H     ?? 5I  "   " ??62   .R?5J  &   ?? $??Center=?67   $??Center=?6T   5K  #   5M      5N     P? 5O      ?@J?IsMouseOver . . ?5P  "   ?? 
 ?@J?RenderTransformOrigin$ 0.5, 0.5&?6J   ?? 5Q  "   
 ? z? 5R  &   ?? 5S  *   $	 1.2Q?69   $	 1.2Q?6F   5U  #   ?? 5V  "   
 ? ??
 ???? 6F   5W     5Y     .??5[     ?? -!   5        $ imgDeleteCard??5[     $!??images/icons/cancelicon.png??6/   5\     ?? 5]     -"   5        $ btnSaveCard??5]  b   $??0??6   $??1??6"   $??80??62   $??80??6>   
?????? $??0q?6w   ??5^     ?? 5_     "	 ??6   .?5`     ?? 
 ? ??
 ???? 67   ?? 5a     
 ? ?? 5b     ?? 5c  "   " ??62   .R?5d  &   ?? $??Center=?67   $??Center=?6T   5e  #   5g      5h     P? 5i      ?@J?IsMouseOver . . ?5j  "   ?? 
 ?@J?RenderTransformOrigin$ 0.5, 0.5&?6J   ?? 5k  "   
 ? z? 5l  &   ?? 5m  *   $	 1.2Q?69   $	 1.2Q?6F   5o  #   ?? 5p  "   
 ? ??
 ???? 6F   5q     5s     .??5u     ?? -#   5        $ imgSaveCard??5u     $??images/icons/saveicon.png??6-   5v     5w     ?? 5x  
   
???   ?$??1q?6%   $??Bottom=?69   $??3??6T   ?? 5y  
   
???   ?$??1q?6%   $??Right=?69   5z     5{     