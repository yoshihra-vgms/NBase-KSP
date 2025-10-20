--元データ全削除
DELETE FROM ms_schedule_category;

--シーケンスリセット
SELECT SETVAL ('ms_schedule_category_schedule_category_id_seq', '1', false);

--Insert
INSERT INTO ms_schedule_category(schedule_category_name) VALUES ('予定実績');
INSERT INTO ms_schedule_category(schedule_category_name) VALUES ('会社');
INSERT INTO ms_schedule_category(schedule_category_name) VALUES ('その他');

