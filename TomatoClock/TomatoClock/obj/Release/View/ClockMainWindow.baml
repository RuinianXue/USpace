   M S B A M L   `   `   `  ����   TomatoClock!    TomatoClock.ClockMainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  $	��300��$	��300��*  $Microsoft.Xaml.Behaviors.Interaction    Triggers  +  %Microsoft.Xaml.Behaviors.EventTrigger     	EventName$ Loaded��*  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command �� *CmdFormLoaded+ � 	Resources  �� MergedDictionaries �� Source   
MDIXButton �� 
TargetType $  MaterialDesignActionToggleButton �� BasedOn	 �� 	Resources��  �� $l fpack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.ToggleButton.xaml?�%�  &	        7  �� " d�8   	 ��  �� $e _pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml?��� $z tpack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.DeepPurple.xaml?��� $s mpack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.Lime.xaml?�.��(� 
 (� Stretch$
 Uniform=�.���  � ColumnDefinitions �� �� $	��43* � � RowDefinitions �� $��* ��� $
��Auto �.+�(� -    (�Name$ Clock��$��
5,10,10,10q�$
 Uniform=�$��1��.��� .+��� -    ��Name$ HiddenComponents��$��Center=�$��Center=� J� Opacity$	 0.7Q�$y�Hidden=�.+��� -    ��Name$ LastTime��$��Center=�$��Center=�$��	加油！��$y�Visible=��� -   $ ThisTime��$��Center=�$��Center=�$��离休息还有��$y�Hidden=��� $��Center=�$��Center=�$��16(�$
��Bold"����� *	TimeCount+�� -    ��Name$ Clocking��$	��200��$	��200��$��Center=�$��Center=�
������ .+�A� $	��200��$	��200��
�������$	��0.3��$��Center=�$��Center=� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     Color$
 Gray��   	Direction$	 315Q�   ShadowDepth$ 3Q�   Opacity$	 0.3Q�����  �� Color$ 	SteelBlue�� �� Opacity$
 0.07Q��� -    ��Name$ 
SecondHand��	 �� X1$	 100��	 �� Y1$ 10��	 �� X2$	 100��	 �� Y2$ 30��
���� ��$��2��z��� -   SecondHandRotationTransform�� �� CenterX$	 100Q� �� CenterY$	 100Q�(� $��2��.���� $��10��
������ .+��� $ 0��$ 0��$	 200��$ 0��$��6������   �� 
StartPoint$	  0,0&�! �� EndPoint$	! 1,1&�.��� " � Color$	" Red��# � Offset$# 0Q�� $" Transparent��$	# 0.5Q�� $
" Blue��$# 1Q�(� $
 Uniform=�$��1��$��2��.���� $��Center=�.+�� $��40��$	��200�� �� $��25 ��� $��50 ��� $��30 ��� $��25 ��� $��20 ��� $��50 � �� $��40 �.+��� -   $ ��Name$$ minus��$��-��$��0��$��5q�$��20���� -	   % ��Name$% TimeSet��$��Center=�$��1��$��3,8,3,8q����� *
MinuterSet+& �� 
IsReadOnly& �� *
IsCounting+.���� -
   $ minute��$��2��$��0,8,0,8q�$�� 分钟��$��10(�$��Center=��� -   $
$ plus��$��+��$��3��$��5q�$��20��d� -   ' d�Name$' State��$	��▷��$��5��$��30��$��30��$��5q�$��Center=�$��Center=�#��[  ( � Cursor$
( Hand��) d� 	IsChecked) �� *
IsCounting+