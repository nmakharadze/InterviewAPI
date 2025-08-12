-- =============================================
-- Dictionary Schema Seed Data
-- =============================================

-- =============================================
-- 1. DictionaryNames ცხრილი (config სქემა)
-- =============================================

-- Dictionary ცხრილების კონფიგურაციის ჩანაწერები
INSERT INTO config.DictionaryNames (TableName, EntityTypeName, CreatedAt, UpdatedAt)
VALUES 
    (N'Genders', N'Gender', GETUTCDATE(), GETUTCDATE()),
    (N'Cities', N'City', GETUTCDATE(), GETUTCDATE()),
    (N'PhoneTypes', N'PhoneType', GETUTCDATE(), GETUTCDATE()),
    (N'RelationTypes', N'RelationType', GETUTCDATE(), GETUTCDATE());

-- =============================================
-- 2. Genders ცხრილი (Dictionary სქემა)
-- =============================================
INSERT INTO Dictionary.Genders (Name, IsActive, CreatedAt, UpdatedAt)
VALUES 
    (N'მამაკაცი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'ქალი', 1, GETUTCDATE(), GETUTCDATE());

-- =============================================
-- 3. Cities ცხრილი (Dictionary სქემა)
-- =============================================
INSERT INTO Dictionary.Cities (Name, IsActive, CreatedAt, UpdatedAt)
VALUES 
    (N'თბილისი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'ბათუმი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'ქუთაისი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'რუსთავი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'გორი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'ზუგდიდი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'ფოთი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'სოხუმი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'ცხინვალი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'თელავი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'ახალციხე', 1, GETUTCDATE(), GETUTCDATE()),
    (N'ოზურგეთი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'საგარეჯო', 1, GETUTCDATE(), GETUTCDATE()),
    (N'მარნეული', 1, GETUTCDATE(), GETUTCDATE()),
    (N'ბოლნისი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'ახალქალაქი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'ქარელი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'კასპი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'წნორი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'დედოფლისწყარო', 1, GETUTCDATE(), GETUTCDATE()),
    (N'ლაგოდეხი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'ახმეტა', 1, GETUTCDATE(), GETUTCDATE()),
    (N'დუშეთი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'მცხეთა', 1, GETUTCDATE(), GETUTCDATE()),
    (N'გარდაბანი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'სიღნაღი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'გურჯაანი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'საჩხერე', 1, GETUTCDATE(), GETUTCDATE()),
    (N'ზესტაფონი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'ვანი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'სამტრედია', 1, GETUTCDATE(), GETUTCDATE()),
    (N'ხონი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'თერჯოლა', 1, GETUTCDATE(), GETUTCDATE()),
    (N'ჩოხატაური', 1, GETUTCDATE(), GETUTCDATE()),
    (N'აბაშა', 1, GETUTCDATE(), GETUTCDATE()),
    (N'სენაკი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'ხობი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'ჩხოროწყუ', 1, GETUTCDATE(), GETUTCDATE()),
    (N'მარტვილი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'ჯვარი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'წალენჯიხა', 1, GETUTCDATE(), GETUTCDATE()),
    (N'ამბროლაური', 1, GETUTCDATE(), GETUTCDATE()),
    (N'ონი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'ლენტეხი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'მესტია', 1, GETUTCDATE(), GETUTCDATE()),
    (N'ცაგერი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'ხარაგაული', 1, GETUTCDATE(), GETUTCDATE()),
    (N'წაღვერი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'ბაკურიანი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'ბორჯომი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'დმანისი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'თეთრიწყარო', 1, GETUTCDATE(), GETUTCDATE()),
    (N'წალკა', 1, GETUTCDATE(), GETUTCDATE()),
    (N'ნინოწმინდა', 1, GETUTCDATE(), GETUTCDATE()),
    (N'ხულო', 1, GETUTCDATE(), GETUTCDATE()),
    (N'შუახევი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'ქედა', 1, GETUTCDATE(), GETUTCDATE()),
    (N'კვარიათი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'უშგული', 1, GETUTCDATE(), GETUTCDATE()),
    (N'მუხრანი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'მანგლისი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'ფასანაური', 1, GETUTCDATE(), GETUTCDATE()),
    (N'შუამთა', 1, GETUTCDATE(), GETUTCDATE());

-- =============================================
-- 4. PhoneTypes ცხრილი (Dictionary სქემა)
-- =============================================
INSERT INTO Dictionary.PhoneTypes (Name, IsActive, CreatedAt, UpdatedAt)
VALUES 
    (N'მობილური', 1, GETUTCDATE(), GETUTCDATE()),
    (N'სახლის', 1, GETUTCDATE(), GETUTCDATE()),
    (N'ოფისის', 1, GETUTCDATE(), GETUTCDATE());

-- =============================================
-- 5. RelationTypes ცხრილი (Dictionary სქემა)
-- =============================================
INSERT INTO Dictionary.RelationTypes (Name, IsActive, CreatedAt, UpdatedAt)
VALUES 
    (N'მეუღლე', 1, GETUTCDATE(), GETUTCDATE()),
    (N'მშობელი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'შვილი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'და', 1, GETUTCDATE(), GETUTCDATE()),
    (N'ძმა', 1, GETUTCDATE(), GETUTCDATE()),
    (N'ბებია', 1, GETUTCDATE(), GETUTCDATE()),
    (N'ბაბუა', 1, GETUTCDATE(), GETUTCDATE()),
    (N'შვილიშვილი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'მეგობარი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'კოლეგა', 1, GETUTCDATE(), GETUTCDATE()),
    (N'მეზობელი', 1, GETUTCDATE(), GETUTCDATE()),
    (N'სხვა', 1, GETUTCDATE(), GETUTCDATE());
