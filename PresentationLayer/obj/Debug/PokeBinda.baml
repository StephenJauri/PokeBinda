   M S B A M L   `   `   `  ????L  HPresentationLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:PresentationLayerPresentationLayer  5   	   !    PresentationLayer.PokeBinda   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:PresentationLayer     5   	   -   5          ? Title$  	PokeBinda??5   	   $	??600??6   $	??800??6(    ? WindowStartupLocation$ CenterScreen=?64   ??5	      ?? 5
   
    ?? EndPoint$
 .6,1&?6    ?? 
StartPoint$
 .5,0&?6.   .??5      ?  ? Color$ 	#FF7A4069??6    ? Offset$ 0.091Q?6-   ? 5      $ 	#FFA7809B??6   $ 1Q?6-   5      .??5      ?  ? ColumnDefinitions 5   
   ?? 5      $	??115 ?6   ?? 5      .+?5   
   ?? -   5         ??Name$ tabSelectionSV??5      $??0??6/   $??Hidden=?6?   $??Disabled=?6d   .??5      ? -   5         ?Name$ gridTabButtons??5      5      5      ?? 5   
   
???   ?$??0??6%   $??1q?65   $??Right=?6I   6h   ? 5   
   $??1??6   	 ? RowDefinitions	 5!      ?? 5"      -   5        
 ??Name$
 header??5"   -   $	??115 ?6    6-   ?? 5#      .+?5&      ? $??0??6    5(      ?? 5)      ?? 5*      $	??115 ?6'   .+?5-      ?? $??0??6    ?? TextAlignment$ Center=?6)   $??Center=?6@   ???#FFC18E$??30(?6p   Pokemon Binder6?   ?? 5/      -   5         ??Name$ 
btnProfile??5/      $??80??6-   $??80??68   $??1??6D   $??0q?6T   
?????? $??Center=?6?   $??Center=?6?    ? 	Resources 50       ?? 
TargetType ?? Property ?? Value??%_   '??       ?? 51      " ??6    .?52      ??  ?@??CornerRadius$ 40??6=   53      54      ??55      ?? 56      " ??6    .?57      ??  ? ??
 ???? 6;   ?? 58       ? ?? 59   "   ?? 5:   &    ?? 
TargetType" ??66   .R?5;   *   ?? $??Center=?6;   $??Center=?6X   5<   '   5>       ?? Triggers 5?      P? 5@   "    P? Property ?@J?IsMouseOver P? Value . . ?5A   &   ??  ?@J?RenderTransformOrigin$ 0.5, 0.5&?6N   ?? 5B   &    ? z? 5C   *   ?? 5D   .    ?? ScaleX$	 1.2Q?6=    ?? ScaleY$	 1.2Q?6J   5F   '   ?? 5G   &    ? ??
 ???? 6J   5H   #   5J      .??5L      ?? $"??images/icons/profileicon.png??6   5M      5N      ?? 5P      $??Bottom=?6   $??0??60   $??1q?6=   
???   ?6Q   ?? 5R      $??1??6   ??5S      ?? 5T      $ 0.5,1&?6*   $ 0.5,0&?6;   .??5U      ? $ 	#FF513252??6'   $ 0Q?69   ? 5V      $ 	#FF6C526D??6'   $ 0.743Q?69   5W      5Y      ? 5Z      -   5        $ InformationSection??5Z      $??1??6/   .+?5[      ?? $??Center=?6   $ Center=?67   $??Center=?6O   $??40(?6l   ???#FFC18E	Welcome?? 6?   to?? 6?   Pokemon Binder6?   5\      5]      5^      5_      