   M S B A M L   `   `   `  ����@  <PLWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:PLWPFPLWPF  5   	   = 9BE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:BE;assembly=BEBE 5   	       PLWPF.AddChildWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:PLWPF     5   	   +BEclr-namespace:BE;assembly=BE     5   	     � Title$  AddChildWindow��5	   	   $	��488��6    $��532.533��6-    � 	Resources 5
         childViewSource��%   &	        �� 5   
   6   5      .��5      �  � ColumnDefinitions 5   
   �� 5       � RowDefinitions 5   
   �� 5      $	��15* �6   �� 5      $��5* �6   .+�5   
   � $��1��6   $��	0,0,0,0.2q�6   $��2��6/    5      �� 5      $��1* �6#   �� 5      $��1* �6#   .+�5      �� -   5        $��Cancel��5      $��27,10,26,10q�6&   $��20(�6;   6I   �� 5      -   5        $
��Add ��5      $��1��6$   $��27,10,26,10q�64   $��20(�6I   6W   5      � 5   
   -   5         �Name$ 	titleGrid��5      .+�5      �� -   5         ��Name$ title��5      $��	Add Chlid��6'     
titleStyle#��[ 68   5      � 5    
   .+�5!      � -   5        $ grid1��5!      $	��Top=�6"   $��	10,79,0,0q�6:   $
��Left=�6M    � DataContext# [  6h   $	��254��6�   $	��426��6�    5"      �� 5#      $
��Auto �6$   �� 5$      $
��Auto �6$   �� 5%      $
��Auto �6$   �� 5&      $
��Auto �6$   �� 5'      $
��Auto �6$   �� 5(      $
��Auto �6$    5*      �� 5+      $
��Auto �6'   �� 5,      $
��Auto �6'   .+�5.      �� $��Center=�6   $��0��63   $��3q�6@   $
��Left=�6K   $��0��6f   $��Date Of Birth:��6v   (  "System.Windows.Controls.DatePicker  5/      -   5          Name$ dateOfBirthDatePicker��5/      $��Center=�6<      SelectedDate$ 	1.15.2018X�6W   $��0��6q   $��3q�6~   $
��Left=�6�   $��1��6�   �� 50      $��Center=�6   $��1��63   $��3q�6@   $
��Left=�6K   $��0��6f   $��First Name:��6v   �� 51      -   5        	 ��Name$	 firstNameTextBox��51      $	��120��64   $��Center=�6@   $��1��6[   $��3q�6h   $��23��6s   $
��Left=�6   $��1��6�   �� 52      $��Center=�6   $��2��63   $��3q�6@   $
��Left=�6K   $��0��6f   $	��Id:��6v   �� 53      -   5        $	 	idTextBox��53      $	��120��6-   $��Center=�69   $��2��6T   $��3q�6a   $��23��6l   $
��Left=�6x   $��1��6�   �� 54      $��Center=�6   $��3��63   $��3q�6@   $
��Left=�6K   $��0��6f   $��Is Special Needs:��6v   �� 55      -	   5        
 ��Name$
 isSpecialNeedsCheckBox��55      $��Center=�6;   $��3��6V   $��3q�6c    d� 	IsChecked$ FalseY�6n   $
��Left=�6�   $��1��6�   $�� ��6�   �� 56      $��Center=�6   $��4��63   $��3q�6@   $
��Left=�6K   $��0��6f   $��choose mother:��6v   �� 57      $��Center=�6   $��5��63   $��3q�6@   $
��Left=�6K   $��0��6f   $��Special Needs:��6v   �� 58      -
   5        $	 specialNeedsTextBox��58      $	��120��67   $��Center=�6C   $��5��6^   $��3q�6k   $��23��6v   $
��Left=�6�   $��1��6�   �� 59      -   5        $��motherCB��59      $��1��6+   $��4��6;   5:      5;      5<      5=      