   M S B A M L   `   `   `  ���� ?  ;GoUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:GoUI.ControlsGoUI.Controls      GoUI.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     2
UIControlsclr-namespace:GoUI.Controls     -     �Name$  MainWindowCtrl�� � Title$ It's Go Time!��$	��400��$	��500��.���  � RowDefinitions �� $��* ��� $
��Auto �.+��� 
�������
���   �$��3,3,3,3q�$��Stretch=�$��Stretch=�$��5,5,5,5q�$��0��._�$   GoUI.Controls.GridBoardControl  -     Name$ GoBoard��$
��Auto��$
��Auto��$��Stretch=�$��Stretch=��� -    ��Name$ 
DebugStack��$��5,5,5,5q�$��1�� �� Orientation$ 
Horizontal=�$��Right=�$y�	Collapsed=�.+��� -    ��Name$ 
DebugLabel��$��Enter Board Size: ��$��0,0,3,0q��� -    ��Name$ DebugTB��$��50��$��0��$��Center=�$��0,0,10,0q��� -    ��Name$ DebugBtn��$��	Debug Me!��