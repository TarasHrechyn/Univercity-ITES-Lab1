   M S B A M L   `   `   `  ����J  FWpfPowerCalcApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:WpfPowerCalcAppWpfPowerCalcApp  5   	   #    WpfPowerCalcApp.NewItemAddDlg   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:WpfPowerCalcApp     5   	     � Title$  Add Item��5   	   $��169.8��6   $��362.4��6)   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $
��285* �6   �� 5      $	��73* �6   .+�5   
   �� -   5         ��Name$ 	buttonAdd��5      $��Ok��6$   $
��Left=�61   $��
273,10,0,0q�6L   $	��Top=�6`   $��75��6x   $��2��6�    �� 	IsDefault . 6�   �� 5   
   -   5        $ buttonCancel��5      $��Cancel��6'   $
��Left=�68   $��
273,35,0,0q�6S   $	��Top=�6g   $��75��6   $��2��6�    �� IsCancel . 6�   �� 5   
   -   5         ��Name$ label��5      $��U��6   $
��Left=�6+   $��32,7,0,0q�6F   $	��Top=�6X   �� 5   
   -   5         ��Name$ 
editorSnom��5      $
��Left=�6&   $��23��6A   $��	75,41,0,0q�6M    �� TextWrapping$
 Wrap=�6`   $��0��6t   $	��Top=�6}   $��65��6�   �� 5   
   -   5        $ 
label_Copy��5      $��S��6$   $
��Left=�60   $��	32,38,0,0q�6K   $	��Top=�6^   �� 5   
   -   5         ��Name$ 
editorUnom��5      	 �� SelectedIndex$	 0��6'   $
��Left=�69   $��75,7,0,0q�6T   $	��Top=�6f   $��65��6~   .@�5      �� $	��220��6   $
��Left=�6)   $��92��6D   �� 5      $	��380��6   $
��Left=�6)   $��92��6D   5      �� 5   
   -   5        $ 
editorName��5      $
��Left=�6&   $��23��6A   $��	77,72,0,0q�6M   $
 Wrap=�6`   $	��Top=�6t   $	��271��6�   $��2��6�   �� 5   
   -   5        $ label_Copy1��5      $��
Назва��6%   $
��Left=�65   $��	34,69,0,0q�6P   $	��Top=�6c   5      5      