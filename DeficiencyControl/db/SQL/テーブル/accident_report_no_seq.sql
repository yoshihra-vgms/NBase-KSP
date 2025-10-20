-- Sequence: accident_report_no_seq

-- DROP SEQUENCE accident_report_no_seq;

CREATE SEQUENCE accident_report_no_seq
  INCREMENT 1
  MINVALUE 1
  MAXVALUE 9223372036854775807
  START 1
  CACHE 1;
ALTER TABLE accident_report_no_seq
  OWNER TO "IGTDefconUser";