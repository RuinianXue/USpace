   M S B A M L   `   `   `  ����D  @UIDisplay, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:UIDisplay.ComponentsUIDisplay.Components  5      #    UIDisplay.Components.TodoUnit   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:UIDisplay.Components     5      S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  5        � ContextMenu  5
      �� 5   
   .@�5      m� -   5        $��deleteMI��5      $��删除��6'   63   5      .��5      �� -   5         ��Name$ 
mainBorder��5      $��60��6!    �� CornerRadius$ 10��6-      PrimaryBackgroundColor#��[  6?   $��0 1.6 0 1.6q�6t    J� Opacity$
 0.85Q�6�    J� RenderTransformOrigin$ 0.5,0.5&�6�   z�5   
   T� 5       T� X$ 0Q�6!    T� Y$ 0Q�6'   ._�5   
   �  � ColumnDefinitions 5      �� 5      $��50 �6#   �� 5      �� 5      $��50 �6#   .+�5      d� -   5         d�Name$ 
Btn_IsDone��5      $��0��6/   $��0q�6?   	 d� 	IsChecked$	 FalseY�6S     ToggleButtonFlip#��[ 6e   $��24��6�   $��24��6�   /  )HandyControl.Controls.StatusSwitchElement
   CheckedElement
 5      �� 5        PrimaryBrush#���  6   ._�5      F�   checked#���  6    �� Stretch$
 Fill=�6@     BackgroundBrush#���  6O   $��12��6x   $��12��6�   $��Center=�6�   $��Center=�6�   5      .��5      �� -   5        $ unCheckPanel��5      #���  6/   ._�5       '  !HandyControl.Controls.SimplePanel  $	 0.8Q�6%   .+�5!      F�   uncheckedcircle#���  6   $
 Fill=�6H     TextSecondaryColor#���  6W   $��1��6�   #���  6�   $��22��6�   $��22��6�   $��Center=�6�   $��Center=�6�   F� 5"      -   5         F�Name$ checkHoverShow��5"      #���  67   $
 Fill=�6X     PrimaryBlueColor#���  6g   $��10��6�   $��10��6�   $��Center=�6�   $��Center=�6�   $y�Hidden=�6�   5#      5%      5&      � 5'      $��1��6    � RowDefinitions 5(      �� 5)      $
��1.7* �6$   �� 5*      .+�5,      #  HandyControl.Controls.TextBox  -   5          Name$ todoContentText��5,      $��0��66   $
��Auto��6C   $
��todo��6P     TextPrimaryColor#���  6\   $��Bottom=�6�   $��16(�6�   
������ 
������  �� 
IsReadOnly . 6�   � 5-      $��1��6   $	 0.8Q�6$    5.      �� 5/      $
��Auto �6+   �� 50      .+�52      �� $��0��6!    �� Orientation$ 
Horizontal=�61   $��Center=�6J   .+�53      F� -   5        $ calenderIcon��53       	 calender#��� 	 65   $
 Fill=�6W   $��10��6f   $��10��6r    
 DangerBrush#��� 
 6}   $��9 0 4 0q�6�   &   HandyControl.Controls.SimpleText  54      -   5          Name$ todoDateTimeText��54   (      Text$ 
2022.11.25��6B   $��Center=�6T      FontSize$ 10(�6o      
Foreground# � 
 6}   55      �� 57      $��1��6!   $ 
Horizontal=�61   $��16 0 0 0q�6J   .+�58        -	   5        $ todoTeammateListTextTitle��58   (   $ 
关联人:��6K   $��Center=�6W   $ 10(�6s   $��40��6�     59      -
   5        $ todoTeammateListText��59   (   $	 无��6F   $��Center=�6O   $ 10(�6j   5:      5;      5<        5=      $��2��6   .+�5>      d� -   5        $ isImportantBtn��5>      $��0��67   $��0q�6G   $	 FalseY�6[   #��[ 6m   $��24��6�   $��24��6�   
������ 
 5?      �� 5@      ._�5A      F�   
starfilled#���  6#   $
 Fill=�6G   #���  6V   $��20��6|   $��20��6�   $��Center=�6�   5B      .��5D      �� -   5        $ 
starBorder��5D      #���   61   ._�5E        .+�5F      F� -   5        $ starPath��5F   #     star#���  65   $
 Fill=�6S     PrimaryGrayColor#���  6b   $��20��6�   $��20��6�   5G      5H      5I      5J      5K      5M      5N      