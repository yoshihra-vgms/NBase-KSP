::���j���pDeficiecnyControl�o�b�N�A�b�v�X�N���v�g

::postgres�C���X�g�[���t�H���_�ֈړ��Bpg_dump���g���̂ł��̏ꏊ�܂ňړ��B
cd C:\Program Files (x86)\pgAdmin III\1.16

::dump�̎��s -U�̌�Ƀ��[�U�[���A -p�Ń|�[�g -Fc�̓t�@�C���o�͂�custom�`���ōs���A�Ƃ����Ӗ��@-Fp�ɂ����sql�ŏo�͉\�B-v�͒������炵�����b�Z�[�W�\�����邱�Ƃ��Ӗ�����̂ŕs�v�Ȃ�������ƁB
:: >�̌�͏o�̓t�@�C���̃p�X�� defcondb���t����.backup�Ńt�@�C�����쐬���Ă���B

::�o�b�N�A�b�v
pg_dump.exe -h 192.168.245.33 -U "postgres" --encoding UTF8 --no-password -p 5432 -Fc -v defcondb > C:\DeficiencyControlBackUp\DefConDB_Monday.backup
