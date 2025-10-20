--元データ全削除
DELETE FROM ms_inspection_category;

--シーケンスリセット
SELECT SETVAL ('ms_inspection_category_inspection_category_id_seq', '1', false);

--Insert
INSERT INTO ms_inspection_category(inspection_category_name) VALUES ('SIRE');
INSERT INTO ms_inspection_category(inspection_category_name) VALUES ('CDI');
