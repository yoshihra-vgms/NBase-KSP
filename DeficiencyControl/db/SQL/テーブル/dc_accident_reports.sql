-- Table: dc_accident_reports

-- DROP TABLE dc_accident_reports;

CREATE TABLE dc_accident_reports
(
  accident_reports_id serial NOT NULL,
  
  accident_id integer,
  order_no integer,
  reports character varying,
  
  
  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_dc_accident_reports PRIMARY KEY (accident_reports_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE dc_accident_reports
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE dc_accident_reports
  IS '事故トラブル報告書提出先テーブル';

-- Trigger: tr_dc_accident_reports on dc_accident_reports

-- DROP TRIGGER tr_dc_accident_reports ON dc_accident_reports;

CREATE TRIGGER tr_dc_accident_reports
  BEFORE INSERT OR UPDATE
  ON dc_accident_reports
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();

