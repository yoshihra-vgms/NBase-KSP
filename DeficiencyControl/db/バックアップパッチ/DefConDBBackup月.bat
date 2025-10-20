::月曜日用DeficiecnyControlバックアップスクリプト

::postgresインストールフォルダへ移動。pg_dumpを使うのでその場所まで移動。
cd C:\Program Files (x86)\pgAdmin III\1.16

::dumpの実行 -Uの後にユーザー名、 -pでポート -Fcはファイル出力をcustom形式で行う、という意味　-Fpにするとsqlで出力可能。-vは長ったらしいメッセージ表示することを意味するので不要なら消すこと。
:: >の後は出力ファイルのパス名 defcondb日付時刻.backupでファイルを作成している。

::バックアップ
pg_dump.exe -h 192.168.245.33 -U "postgres" --encoding UTF8 --no-password -p 5432 -Fc -v defcondb > C:\DeficiencyControlBackUp\DefConDB_Monday.backup
