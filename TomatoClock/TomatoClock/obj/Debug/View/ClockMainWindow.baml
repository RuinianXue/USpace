   M S B A M L   `   `   `  ����  TomatoClock!    TomatoClock.ClockMainWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5   	   $	��300��5   	   $	��300��6   *  $Microsoft.Xaml.Behaviors.Interaction    Triggers  5      +  %Microsoft.Xaml.Behaviors.EventTrigger  5   
      	EventName$ Loaded��6   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5      2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command 6$   �� *CmdFormLoaded+5	       � 	Resources 5       �� MergedDictionaries �� Source   
MDIXButton �� 
TargetType $  MaterialDesignActionToggleButton �� BasedOn	 �� 	Resources�� 5   
    5      �� 5      $l fpack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.ToggleButton.xaml?�6%   %�  &	        7  �� 5      " d�6'   8   	 5      �� 5       5      �� 5      $e _pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml?�61   �� 5      $z tpack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.DeepPurple.xaml?�61   �� 5      $s mpack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.Lime.xaml?�61   5      5      5      .��5       (� 
 (� Stretch$
 Uniform=�6   .��5!   
   �  � ColumnDefinitions 5"      �� 5#      �� 5$      $	��43* �6#    � RowDefinitions 5&      �� 5'      $��* �6    �� 5(      $
��Auto �6    .+�5-      (� -   5         (�Name$ Clock��5-      $��
5,10,10,10q�6%   $
 Uniform=�69   $��1��6K   .��5.      � .+�50      �� -   5         ��Name$ HiddenComponents��50   !   $��Center=�6;   $��Center=�6X    J� Opacity$	 0.7Q�6s   $y�Hidden=�6�   .+�51      �� -   5         ��Name$ LastTime��51   \   $��Center=�6$   $��Center=�6A   $��	加油！��6n   $y�Visible=�6y   �� 52      -   5        $ ThisTime��52   \   $��Center=�6$   $��Center=�6A   $��离休息还有��6n   $y�Hidden=�6{   �� 53      $��Center=�6$   $��Center=�6A   $��16(�6w   $
��Bold"�6�   ��6\   �� *	TimeCount+6�   54      �� 55      -   5         ��Name$ Clocking��55      $	��200��6/   $	��200��6;   $��Center=�6H   $��Center=�6e   
������ .+�57      A� $	��200��58      $	��200��59      
�������$	��0.3��5;      $��Center=�5<      $��Center=�5=       J� Effect 5>      3  -System.Windows.Media.Effects.DropShadowEffect  5?   "      Color$
 Gray��63      	Direction$	 315Q�6@      ShadowDepth$ 3Q�6P      Opacity$	 0.3Q�6`   ��5A      �� 5B   "    �� Color$ 	SteelBlue��62    �� Opacity$
 0.07Q�6D   5D      �� 5E      -   5         ��Name$ 
SecondHand��5E      	 �� X1$	 100��63   	 �� Y1$ 10��6<   	 �� X2$	 100��6D   	 �� Y2$ 30��6M   
���� ��$��2��6e   z�5F      �� 5G   "   -   5        SecondHandRotationTransform�� �� CenterX$	 100Q�5G   W    �� CenterY$	 100Q�6e   5I      5K      5L      5M      (� 5O      $��2��6   .��5P      �� $��10��6   
������ .+�5Q      �� $ 0��6   $ 0��6"   $	 200��6)   $ 0��62   $��6��69   ��5R      �� 5S        �� 
StartPoint$	  0,0&�62   ! �� EndPoint$	! 1,1&�6C   .��5T   "   � " � Color$	" Red��6/   # � Offset$# 0Q�6;   � 5U   "   $" Transparent��6/   $	# 0.5Q�6C   � 5V   "   $
" Blue��6/   $# 1Q�6<   5W      5Y      5Z      5[      (� 5]      $
 Uniform=�6   $��1��6(   $��2��65   .��5_      �� $��Center=�6   .+�5a      � $��40��6   $	��200��6'    5b      �� 5c      $��25 �6/   �� 5d      $��50 �6/   �� 5e      $��30 �6/   �� 5f      $��25 �6/   �� 5g      $��20 �6/   �� 5h      $��50 �6/    5j      �� 5k      $��40 �6,   .+�5m      �� -   5        $ ��Name$$ minus��5m   !   $��-��60   $��0��6<   $��5q�6L   $��20��6W   6c   �� 5n      -	   5        % ��Name$% TimeSet��5n   "   $��Center=�6O   $��1��6q   $��3,8,3,8q�6�   ��63   �� *
MinuterSet+& �� 
IsReadOnly& 6�   �� *
IsCounting+.��6�   5o      �� 5p      -
   5        $ minute��5p   $   $��2��64   $��0,8,0,8q�6D   $�� 分钟��6U   $��10(�6`   $��Center=�6n   �� 5q      -   5        $
$ plus��5q   !   $��+��6/   $��3��6;   $��5q�6K   $��20��6V   6b   d� 5r      -   5        ' d�Name$' State��5s      $	��▷��5t      $��5��5u      $��30��5v      $��30��5w      $��5q�5x      $��Center=�5y      $��Center=�5z      #��[  5{      ( � Cursor$
( Hand��5~   5   ) d� 	IsChecked) 5|      �� *
IsCounting+5~   5   5      5�      5�      5�      5�      5�      