   M S B A M L   `   `   `  ????L  HPresentationLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:PresentationLayerPresentationLayer  5   	   -    'PresentationLayer.ConfirmPasswordPrompt   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:PresentationLayer     5   	   -   5          ? Title$  
Enter Text??5   	   $	??135??6   $	??200??6)    ? 
ResizeMode$ NoResize=?65    ? WindowStartupLocation$ CenterOwner=?6b   ??5	      ?? 5
   
    ?? EndPoint$
 .6,1&?6    ?? 
StartPoint$
 .5,0&?6.   .??5      ?  ? Color$ 	#FF7A4069??6    ? Offset$ 0.091Q?6-   ? 5      $ 	#FFA7809B??6   $ 1Q?6-   5      .??5      ?  ? RowDefinitions 5   
   ?? 5      $??30 ?6   ?? 5      $??30 ?6   ?? 5      .+?5   
   ?? -   5         ??Name$ tbkText??5      $??0??6%   $
??10 5q?62   $??Enter this text??6@   $??Center=?6W   $??Center=?6t   G? 5   
   -   5        	 G?Name$	 tbxInput??5   C   
?????? 
???   ?$??1??6U   $
??10 5q?6b   $??Center=?6p   $??Center=?6?   ? 5   
   $??2??6   
 ? ColumnDefinitions
 5      ?? 5      ?? 5      .+?5      ?? -   5         ??Name$ btnOK??5      $??OK??6$   $??	10 5 5 10q?61   $??0??6D   6T   ?? 5      -   5        $ 	btnCancel??5      $??Cancel??6(   $??	5 5 10 10q?69   $??1??6L   6\   5      5      5       