@echo off
cls

"C:\Program Files (x86)\Java\jdk1.8.0_121\bin\javac.exe" CalendarEvent.java -d . -source 1.7 -target 1.7 -classpath "C:\Users\Onur\AppData\Local\Android\Sdk\platforms\android-27\android.jar";
"C:\Program Files (x86)\Java\jdk1.8.0_121\bin\jar.exe" cvfM ../CalendarEvent.jar calendarevent
rd /s /q calendarevent
