   M S B A M L   `   `   `  ����>  :TMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:TMS.views	TMS.views  5          TMS.views.SettingsView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      )localclr-namespace:TMS.views     5      U QMahApps.Metro, Version=1.5.0.23, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f;Controls,http://metro.mahapps.com/winfx/xaml/controls  5      .��5      �� $
��Auto=�6   .��5   
   �   � ColumnDefinitions  5      �� 5      $	��115 �6#   �� 5      $��* �6#   .+�5      ��    task#���   6   $��13(�65   $
��Left=�6C   $	��Top=�6^   $��15,15,15,15q�6v   $��0��6�   �� 5      -   5         ��Name$ tb_name��5      $
��Auto��6@   $
��Left=�6M   $��Center=�6o   $��Stretch=�6�   $	��Top=�6�   $��
0,15,15,15q�6�   $��1��6�   �� 5        deadline_task#���  6   $��13(�6>   $
��Left=�6L   $	��Top=�6g   $��15,60,15,15q�6   $��0��6�   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ dp_deadline��5         FirstDayOfWeek$ Monday=�6.   $��
0,60,15,15q�6F   $��Stretch=�6Z   $	��Top=�6x   $��1��6�   $
��Auto��6�     date_tooltip*  $MahApps.Metro.Controls.TextBoxHelper   	Watermark# �  6�   �� 5        members_task#���  6   $��13(�6=   $
��Left=�6K   $	��Top=�6f   $��15,105,15,15q�6~   $��0��6�   �� 5      -   5         ��Name$ listView��5      $��Stretch=�6)     select_members# �  6t    �� SelectionMode$ Multiple=�5      $��0,105,15,15q�60   $	��Top=�6E   $��1q�6]   $��1��6q   $	��160��6�   
�������6�   �� 5        description_task#���  6   $��13(�6A   $
��Left=�6O   $	��Top=�6j   $��15,280,15,15q�6�   $��0��6�   �� 5      -   5         ��Name$ tb_desc��5      $��1��6+   $	��160��6;   $��Stretch=�6H   $	��Top=�6f   $ �� HorizontalScrollBarVisibility$ Hidden=�6~   "	 �� VerticalScrollBarVisibility$	 Visible=�5      $��0,285,15,15q�68   $	��265��6M   
 � 	Resources
 5       �� 
TargetType �� Property �� Value��%X   'J�       �� 5      " J�6   .�5       ��  �@��Margin$ 0q�63   5!      5"      5#      �� 5%        repetition_task#���  6   {�.  $��13(�6R   $
��Left=�6`   $	��Top=�6{   $��15,465,15,15q�6�   $��0��6�   �� 5&      -   5         ��Name$ task_repetition��5&      {�.  $��Stretch=�6B   $��1��6`   $	��Top=�6�   $��
0,465,15,0q�6�   .@�5'      �� $
��None��6   �� 5(      $��	Every Day��6   �� 5)      $��Every 7 days��6   �� 5*      $��Every 30 days��6   �� 5+      $��Specific days��6   5,      &   System.Windows.Controls.Calendar  5-      -   5          Name$ calendar��5-      {�.     SelectionMode$ MultipleRange=�6;   $��1��6Y   $
��Left=�6i   $	��Top=�6�   $��0,495,15,15q�6�   5/      50      52      