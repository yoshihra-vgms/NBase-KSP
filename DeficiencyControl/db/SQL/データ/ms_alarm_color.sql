--元データ全削除 
DELETE FROM ms_alarm_color;

--シーケンスリセット
SELECT SETVAL ('ms_alarm_color_alarm_color_id_seq', '1', false);

--Insert
INSERT INTO ms_alarm_color(day_count, color_r, color_g, color_b, comment) VALUES (180, 255, 255, 128, '');
INSERT INTO ms_alarm_color(day_count, color_r, color_g, color_b, comment) VALUES (90, 255, 180, 180, '');
INSERT INTO ms_alarm_color(day_count, color_r, color_g, color_b, comment) VALUES (30, 255, 30, 30, '');