--元データ全削除
DELETE FROM ms_schedule_kind;

--シーケンスリセット
SELECT SETVAL ('ms_schedule_kind_schedule_kind_id_seq', '1', false);

--Insert
INSERT INTO ms_schedule_kind(schedule_category_id, schedule_kind_name) VALUES (1, '検船');
INSERT INTO ms_schedule_kind(schedule_category_id, schedule_kind_name) VALUES (1, 'SMC/ISSC');
INSERT INTO ms_schedule_kind(schedule_category_id, schedule_kind_name) VALUES (1, '内部監査');
INSERT INTO ms_schedule_kind(schedule_category_id, schedule_kind_name) VALUES (1, '入渠');
INSERT INTO ms_schedule_kind(schedule_category_id, schedule_kind_name) VALUES (2, 'DOC・NK審査');
INSERT INTO ms_schedule_kind(schedule_category_id, schedule_kind_name) VALUES (2, 'ISM');
INSERT INTO ms_schedule_kind(schedule_category_id, schedule_kind_name) VALUES (2, 'ISO');
INSERT INTO ms_schedule_kind(schedule_category_id, schedule_kind_name) VALUES (2, 'TMSA');



