   M S B A M L   `   `   `  ????L  HPresentationLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:PresentationLayerPresentationLayer  5      (    "PresentationLayer.ViewAllPokemonUC   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:PresentationLayer     5      .??5	      ?   ? ColumnDefinitions  5
   
   ?? 5      ?? 5      $	??100 ?6   .+?5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ PokemonGrid??5   ?      HeadersVisibility$ Column=?6      
IsReadOnly .    AutoGenerateColumns .     SelectionMode$ Single=?6\      MinColumnWidth$ 40Q?6s      ColumnHeaderStyle 5      ?? 5       ?? 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6   .?5      ?? 	 ?? Property	 ? ??
 ?? Value
 5      ?? 5       ?? EndPoint$ 0.5,1&?62    ?? 
StartPoint$ 0.5,0&?6C   .??5   "   ?  ? Color$ 	#FF513252??6/    ? Offset$ 0Q?6A   ? 5   "   $ 	#FF6C526D??6/   $ 0.743Q?6A   5      5      ?? 5      	 ? ??
 ?#FFC18E63   ?? 5      	 ? ??$
 15(?61   5         RowStyle 5      ?? 5      )  #System.Windows.Controls.DataGridRow"  6   .?5       ?? 	 ? ??
 5!      ?? 5"       ?? Color$ Transparent??6.   5$      ?? 5%      	 ? ??
 ?#FFC18E63   ?? 5&      	 ? ??$
 15(?61   5'      ??5)      ?? 5*      "  6   .?5+      ?? 	 ? ??
 5,      ?? 5-      $
 .6,1&?62   $
 .5,0&?6B   .??5.   "   ? $ 	#FF7A4069??6/   $ 0.091Q?6A   ? 5/   "   $ 	#FFA7809B??6/   $ 1Q?6A   50      52      53         Columns 55      0  *System.Windows.Controls.DataGridTextColumn  56      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 6G      Header$ 	Pokedex #??6S      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   ?? *PokedexNumber+6f     57      $ 60 6=   $	 Gen??6H    .  6%   ?? *Gen+6U     58      $	 150 6>   $
 Name??6J    .  6%   ?? *Name+6X     59      $ 60 6A   $ Active??6L    .  6%   ?? *Active+6\     5:      $ 60 6C   $ Released??6N    .  6%   ?? *
Released+6`   5<      ? 5=   
   $??1??6    ? RowDefinitions 5>      ?? 5?      $	??100 ?6    ?? 5@      $	??100 ?6    ?? 5A      ?? 5B      $	??100 ?6    .+?5D      ?? -   5         ??Name$ btnAddPokemon??5D   b   $??0??6   $??0??6"   $??70??62   $??70??6>   
?????? $??0q?6y   ??5E      ?? 5F      " ??6   .?5G      ?? 	 ? ??

 ???? 67   ?? 5H      	 ? ??
 5I      ?? 5J   "    ?? 
TargetType" ??62   .R?5K   &   ?? $??Center=?67   $??Center=?6T   5L   #   5N       ?? Triggers 5O      P? 5P       P? Property ?@J?IsMouseOver P? Value . . ?5Q   "   ?? 	 ?@J?RenderTransformOrigin$
 0.5, 0.5&?6J   ?? 5R   "   	 ? z?
 5S   &   ?? 5T   *    ?? ScaleX$	 1.2Q?69    ?? ScaleY$	 1.2Q?6F   5V   #   ?? 5W   "   	 ? ??

 ???? 6F   5X      5Z      .??5\      ?? -   5         ??Name$ 
imgAddCard??5\      $??images/icons/plusicon.png??6,   5]      ?? 5^      -   5        $ btnEditPokemon??5^   b   $??1??6   $??0??6"   $??70??62   $??70??6>   
?????? $??0q?6z   ??5_      ?? 5`      " ??6   .?5a      ?? 	 ? ??

 ???? 67   ?? 5b      	 ? ??
 5c      ?? 5d   "   " ??62   .R?5e   &   ?? $??Center=?67   $??Center=?6T   5f   #   5h       5i      P? 5j       ?@J?IsMouseOver . . ?5k   "   ?? 	 ?@J?RenderTransformOrigin$
 0.5, 0.5&?6J   ?? 5l   "   	 ? z?
 5m   &   ?? 5n   *   $	 1.2Q?69   $	 1.2Q?6F   5p   #   ?? 5q   "   	 ? ??

 ???? 6F   5r      5t      .??5v      ?? $??images/icons/penicon.png??6   5w      ?? 5z      -   5        $ 	btnSelect??5z   v   $y?Hidden=?6   $??3??6)   $??0??66   $??70??6F   $??70??6R   
?????? $??0q?6?   ??5{      ?? 5|      " ??6   .?5}      ?? 	 ? ??

 ???? 67   ?? 5~      	 ? ??
 5      ?? 5?   "   " ??62   .R?5?   &   ?? $??Center=?67   $??Center=?6T   5?   #   5?       5?      P? 5?       ?@J?IsMouseOver . . ?5?   "   ?? 	 ?@J?RenderTransformOrigin$
 0.5, 0.5&?6J   ?? 5?   "   	 ? z?
 5?   &   ?? 5?   *   $	 1.2Q?69   $	 1.2Q?6F   5?   #   ?? 5?   "   	 ? ??

 ???? 6F   5?      5?      .??5?      ?? $(??"images/icons/checkselectedicon.png??6   5?      5?      5?      5?      