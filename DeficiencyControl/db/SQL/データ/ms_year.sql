--元データ全削除
DELETE FROM ms_year;

--シーケンスリセット
SELECT SETVAL ('ms_year_year_id_seq', '1', false);

--Insert
INSERT INTO ms_year(year, start_date, end_date) VALUES (2017, '2017/4/1', '2018/3/31');
INSERT INTO ms_year(year, start_date, end_date) VALUES (2018, '2018/4/1', '2019/3/31');
INSERT INTO ms_year(year, start_date, end_date) VALUES (2019, '2019/4/1', '2020/3/31');
INSERT INTO ms_year(year, start_date, end_date) VALUES (2020, '2020/4/1', '2021/3/31');
INSERT INTO ms_year(year, start_date, end_date) VALUES (2021, '2021/4/1', '2022/3/31');
INSERT INTO ms_year(year, start_date, end_date) VALUES (2022, '2022/4/1', '2023/3/31');
INSERT INTO ms_year(year, start_date, end_date) VALUES (2023, '2023/4/1', '2024/3/31');
INSERT INTO ms_year(year, start_date, end_date) VALUES (2024, '2024/4/1', '2025/3/31');
INSERT INTO ms_year(year, start_date, end_date) VALUES (2025, '2025/4/1', '2026/3/31');



