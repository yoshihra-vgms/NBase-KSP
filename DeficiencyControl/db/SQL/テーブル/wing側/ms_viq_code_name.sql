-- Table: ms_viq_code_name

-- DROP TABLE ms_viq_code_name;

CREATE TABLE ms_viq_code_name
(
  viq_code_name_id serial NOT NULL,
  viq_code_name character varying,
  description character varying,
  description_eng character varying,
  order_no integer,



  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_ms_viq_code_name PRIMARY KEY (viq_code_name_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE ms_viq_code_name
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE ms_viq_code_name
  IS 'VIQ Code名前マスタ';

-- Trigger: tr_ms_viq_code_name on ms_viq_code_name

-- DROP TRIGGER tr_ms_viq_code_name ON ms_viq_code_name;

CREATE TRIGGER tr_ms_viq_code_name
  BEFORE INSERT OR UPDATE
  ON ms_viq_code_name
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();