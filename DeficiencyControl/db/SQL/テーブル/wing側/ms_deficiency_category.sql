-- Table: ms_deficiency_category

-- DROP TABLE ms_deficiency_category;

CREATE TABLE ms_deficiency_category
(
  deficiency_category_id serial NOT NULL,
  deficiency_category_no character varying,
  deficiency_category_name character varying,

  delete_flag boolean DEFAULT false,
  create_ms_user_id character varying(40),
  update_ms_user_id character varying(40),
  create_date timestamp without time zone,
  update_date timestamp without time zone,
  CONSTRAINT pk_ms_deficiency_category PRIMARY KEY (deficiency_category_id )
)
WITH (
  OIDS=FALSE
);
ALTER TABLE ms_deficiency_category
  OWNER TO "IGTDefconUser";
COMMENT ON TABLE ms_deficiency_category
  IS 'DeficiencyCodeカテゴリマスタ';

-- Trigger: tr_ms_deficiency_category on ms_deficiency_category

-- DROP TRIGGER tr_ms_deficiency_category ON ms_deficiency_category;

CREATE TRIGGER tr_ms_deficiency_category
  BEFORE INSERT OR UPDATE
  ON ms_deficiency_category
  FOR EACH ROW
  EXECUTE PROCEDURE "TrAutoDateUpdate"();