-- Table: ms_viq_code

-- DROP TABLE ms_viq_code;

CREATE TABLE ms_viq_code
(
  viq_code_id serial NOT NULL,
  viq_code_name_id integer,
  viq_version_id integer,
  viq_code character varying,
  description character varying,
  description_eng character varying,
  order_no integer,



  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_ms_viq_code PRIMARY KEY (viq_code_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE ms_viq_code
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE ms_viq_code
  IS 'VIQ Codeマスタ';

-- Trigger: tr_ms_viq_code on ms_viq_code

-- DROP TRIGGER tr_ms_viq_code ON ms_viq_code;

CREATE TRIGGER tr_ms_viq_code
  BEFORE INSERT OR UPDATE
  ON ms_viq_code
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();