   M S B A M L   `   `   `  ����D  @UIDisplay, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:UIDisplay.PagesUIDisplay.Pages  5   	   V RCefSharp.Wpf, Version=119.4.30.0, Culture=neutral, PublicKeyToken=40c4b6fc221f4138A0clr-namespace:CefSharp.Wpf;assembly=CefSharp.WpfCefSharp.Wpf 5   	   (    "UIDisplay.Pages.MarkdownEditorPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:UIDisplay.Pages     5   	   @cef0clr-namespace:CefSharp.Wpf;assembly=CefSharp.Wpf     5   	     N� Title$9  3MarkdownEditor - a basic markdown viewer and editor��5	   	   $	��720��6E   $	��900��6R    N� 
Background
 ����� N� 
Foreground
 �����.��5      � $��0,0,10,0q�6    � ColumnDefinitions 5   
   �� 5       � RowDefinitions 5   
   �� 5      $��38 �6   �� 5      .+�5   
   �� -   5        $��saveBtn��5      $��28��6    $��0q�6A    � ToolTip$
 Save��6M   $
��Left=�6\   $��13,0,0,0q�6w   $��30��6�   .��6�   💾6�   �� 5   
   -   5        $��	saveAsBtn��5      $��48,0,0,0q�6"   $��28��64   $��0q�6?   $ Save As��6K   $
��Left=�6u   $��30��6�   ��5      �� 5       �� Color$ LightSkyBlue��6"   .��5   !   💾5      �� 5   
   -   5        $��newBtn��5      $��	116,0,0,0q�6   $��28��62   $��15(�6R   $��0q�6`   $ New Markdown File��6l   $
��Left=�6�   $��30��6�   .��6�   📄6�   �� 5   
   -   5        $��	exportBtn��5      $��82,0,0,0q�6"   $��28��64   $��15(�6W   $��0q�6e   $ Export to HTML��6q   $
��Left=�6�   $��30��6�   .��6�   📤6�   �� 5   
   -   5        $��openBtn��5      $��	150,0,0,0q�6    $��28��63   $��0q�6T   $ 	Open File��6`   $
��Left=�6t   $��30��6�   .��6�   📂6�   �� 5   
   -   5        $��openFolderBtn��5      $��	184,0,0,0q�6&   $��28��69   $��0q�6D   $ Open Folder��6P   $
��Left=�6�   $��30��6�   .��6�   	🗃️6�   �� 5    
   -   5        $��previewWindowBtn��5       $��28��6)   $��0q�64   $ Toggle Preview��6@   
����·�$��30��6�   $
��Left=�6�   $��	825,0,0,0q�6�   .��6�   🌐5!      �� 5"   
   -   5        $��mainWindowBtn��5"      $��28��6&   $��0q�61   $ Toggle Markdown Source��6=   
����·�$��30��6�   $
��Left=�6�   $��	793,0,0,0q�6�   .��6�   .md5#      �� 5$   
   -	   5        $��explorerWindowBtn��5$      $��28��6*   $��0q�65   $ Toggle 'Open Folder'��6A   
����·�$��30��6�   $
��Left=�6�   $��	759,0,0,0q�6�   .��6�   📂5%      � 5'   
   $��1��6    5(      �� 5)      -
   5         �Name$ 
mainColumn��5)   #   $
��162* �65   �� 5*      -   5        $ previewColumn��5*   #   $
��283* �68   .+�5-      �� $��10, 32, 5, 47q�6   $
��Auto=�62   �� 5.      -   5        $��txtRaw��5.       �� 
AcceptsTab . 	 �� AcceptsReturn	 . $��
10,5,10,10q�6K   $��10q�6_   $��Consolas)�6l   $��16(�6�   ���#FFFFFF
���   �$��0q�6�   
 �� TextWrapping$

 Wrap=�6�   $	��Top=�6�   .��6�    � 	Resources 50       �� 
TargetType �� Property �� Value��%^   '��       �� 51      " ��6   .�52      ��  �@��CornerRadius$ 5��69   53      54      .��6%   55      �� 57      $��5��6   $
��Left=�6%    �� ShowsPreview .  �� ResizeBehavior$ PreviousAndNext=�58   /   $y�Visible=�6P   $��1��6e   %  CefSharp.Wpf.ChromiumWebBrowser  5:      -   5        $��browser��5:   %   $��
315,5,0,10q�64   $
��Left=�6H   $	��565��6c   $��2��6o   5;      5<      5=      