-- Table: ms_deficiency_code

-- DROP TABLE ms_deficiency_code;

CREATE TABLE ms_deficiency_code
(
  deficiency_code_id serial NOT NULL,
  deficiency_code_name character varying,
  defective_item character varying,
  deficiency_category_id integer,

  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_ms_deficiency_code PRIMARY KEY (deficiency_code_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE ms_deficiency_code
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE ms_deficiency_code
  IS 'DeficiencyCodeマスタ';

-- Trigger: tr_ms_deficiency_code on ms_deficiency_code

-- DROP TRIGGER tr_ms_deficiency_code ON ms_deficiency_code;

CREATE TRIGGER tr_ms_deficiency_code
  BEFORE INSERT OR UPDATE
  ON ms_deficiency_code
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();