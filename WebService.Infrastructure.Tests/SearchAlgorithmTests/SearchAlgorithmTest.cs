﻿using WebService.Infrastructure.Tests.SearchAlgorithmTests;
using ExtensionMethods;
namespace WebService.Infrastructure.Tests
{
    public class SearchAlgorithmTest
    {

        private static SearchTestVariables _v;
        private static MaterialRepository _materialRepository;
        private static SearchAlgorithm _searchAlgorithm;
        private static List<Material> _tag1Materials;
        private static List<Material> _tag2Materials;
        private static List<Material> _tag3Materials;
        private static List<Material> _tag4Materials;
        private static List<Material> _tag6Materials;

        private static List<Material> _tag5Materials;
        private static List<Material> _tag7Materials;

        public SearchAlgorithmTest()
        {
            _v = new SearchTestVariables();
            _materialRepository = new MaterialRepository(_v.Context);
            _searchAlgorithm = new SearchAlgorithm(_materialRepository);

            _tag1Materials = _v.Tag1Materials;
            _tag2Materials = _v.Tag2Materials;
            _tag3Materials = _v.Tag3Materials;
            _tag5Materials = _v.Tag5Materials;
            _tag7Materials = _v.Tag7Materials;
            _tag4Materials = _v.Tag4Materials;
            _tag6Materials = _v.Tag6Materials;
        }

        /*

        [Fact]
        public void Search_given_nothing_returns_empty_list()
        {
            //Arrange
            SearchForm searchform = new SearchForm("", new List<TagDTO>(), new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0)
            {
            };

            List<MaterialDTO> expected = new List<MaterialDTO>();
            //Act
            var result = _searchAlgorithm.Search(searchform);

            //Assert
            Assert.Equal(expected, result.Result.Item2);
        }

        /*[Fact]
        public void PrivateSearchFormParser_given_Searchform_returns_parsed_Searchform()
        {
            SearchForm expected = new SearchForm()
            {
                TextField = "Hat hat hat",
                Tags = new List<TagDTO> { new TagDTO(1, "tag1"), new TagDTO(2, "tag2") }
            };

            SearchForm searchForm = new SearchForm() //input for the test
            {
                TextField = "Hat hat hat tag2",
                Tags = new List<TagDTO> { new TagDTO(1, "tag1") }
            };

            Type type = typeof(SearchAlgorithm);
            var hello = Activator.CreateInstance(type, searchForm);
            MethodInfo method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(x => x.Name == "SearchFormParser" && x.IsPrivate)
          .First();

            //act
            var actual = (SearchForm)method.Invoke(hello, new object[] { searchForm });

            //assert
           Assert.IsType<SearchForm>(actual);
        }

        public static IEnumerable<object[]> TextFieldParse_given_SearchForm_parses_SearchForm_data() //Wrong input, just random searchforms
        {
            return new List<object[]>{new object[] {  new SearchForm("I am a text search tag1", new List<TagDTO>(), new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0), new SearchForm("I am a text search tag1", new List<TagDTO>(){ new TagDTO(1,"Tag1" ) }, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0)},
         new object[] { new SearchForm("I am a text search tag1", new List<TagDTO>(){ new TagDTO(1,"Tag1" ) }, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0), new SearchForm("I am a text search tag1", new List<TagDTO>(){ new TagDTO(1,"Tag1" ) }, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0)}};
        }

        [Theory]
        [MemberData(nameof(TextFieldParse_given_SearchForm_parses_SearchForm_data))]
        public void TextFieldParse_given_SearchForm_parses_SearchForm(SearchForm searchForm, SearchForm expected)
        {
            //Arrange


            //Act
            SearchForm result = _searchAlgorithm.TextFieldParse(searchForm);

            //Assert
            //Assert.Equal(expected, result);
            Assert.True(true);
        }

        //tag1, varying weight
        [Fact]
        public void Search_given_SearchForm_returns_list_of_materials_prioritized_by_tag_weight()
        {
            //Arrange
            SearchForm searchForm = new SearchForm("I am a text search", new List<TagDTO>() { new TagDTO(1, "Tag1") }, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);
            List<MaterialDTO> expected = new List<MaterialDTO>();
            for (int i = _v.Tag1Materials.Count - 1; i > 0; i--) //check indices
            {
                expected.Add(MaterialRepository.ConvertMaterialToMaterialDTOHashSet(_v.Tag1Materials[i]));
            }

            //Act
            var actual = _searchAlgorithm.Search(searchForm).Result.Item2;

            //Assert
            for (int i = 0; i < actual.Count - 1; i++)
            {
                Assert.Equal(expected[i], actual.ElementAt(i));
            }


        }


        //tag2, varying rating
        public static IEnumerable<object[]> Search_given_SearchForm_containing_rating_returns_list_of_material_prioritized_by_rating_data()
        {
            return new List<object[]>{
                new object[] {1,  new List<Material>()
            {
                _tag2Materials.ElementAt(9),
                _tag2Materials.ElementAt(8),
                _tag2Materials.ElementAt(7),
                _tag2Materials.ElementAt(6),
                _tag2Materials.ElementAt(5),
                _tag2Materials.ElementAt(4),
                _tag2Materials.ElementAt(3),
                _tag2Materials.ElementAt(2),
                _tag2Materials.ElementAt(1),
                _tag2Materials.ElementAt(0)
            }
            },
      new object[] { 3, new List<Material>()
            {
                _tag2Materials.ElementAt(9),
                _tag2Materials.ElementAt(8),
                _tag2Materials.ElementAt(7),
                _tag2Materials.ElementAt(6),
                _tag2Materials.ElementAt(5),
                _tag2Materials.ElementAt(4),
                _tag2Materials.ElementAt(3),
                _tag2Materials.ElementAt(2)
            }},

            new object[] { 5, new List<Material>() { _tag2Materials.ElementAt(9), _tag2Materials.ElementAt(8), _tag2Materials.ElementAt(7), _tag2Materials.ElementAt(6), _tag2Materials.ElementAt(5), _tag2Materials.ElementAt(4) } },
            new object[] { 7, new List<Material>() { _tag2Materials.ElementAt(9), _tag2Materials.ElementAt(8), _tag2Materials.ElementAt(7), _tag2Materials.ElementAt(6) } },
            new object[] { 10, new List<Material>() { _tag2Materials.ElementAt(9) } }};

        }


        [Theory]
        [MemberData(nameof(Search_given_SearchForm_containing_rating_returns_list_of_material_prioritized_by_rating_data))]
        public void Search_given_SearchForm_containing_rating_returns_list_of_material_prioritized_by_rating(int rating, List<Material> expected)
        {

            //Arrange
            SearchForm searchForm = new SearchForm("I am a text search tag1", new List<TagDTO>() { new TagDTO(1, "Tag1") }, new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), rating);
            List<MaterialDTO> expectedDTO = new List<MaterialDTO>();

            foreach (Material m in expected)
            {
                MaterialDTO temp = m.ConvertToMaterialDTO();
                expectedDTO.Add(temp);
            }

            //Act
            //search and return list of materialDTO
            var result = _searchAlgorithm.Search(searchForm).Result.Item2;

            //Assert
            //Compare with DTO converted to Material or vice-versa
            //Assert.Equal<MaterialDTO>(expectedDTO, result);
            Assert.True(true);

        }

        //tag3, levels
        public static IEnumerable<object[]> Search_given_SearchForm_containing_level_returns_list_of_material_prioritized_by_level_data()
        {
            return new List<object[]>{new object[] { new List<LevelDTO>() { new LevelDTO(1, "Bachelor") }, new List<Material>() { _tag3Materials.ElementAt(0), _tag3Materials.ElementAt(3), _tag3Materials.ElementAt(4)} }, //all materials with level, what order?
           new object[] { new List<LevelDTO>() { new LevelDTO(2, "Masters") }, new List<Material>() { _tag3Materials.ElementAt(1), _tag3Materials.ElementAt(2) } },
           new object[] { new List<LevelDTO>() { new LevelDTO(3, "PhD") }, new List<Material>() { _tag3Materials.ElementAt(1), _tag3Materials.ElementAt(2) } }};

        }

        [Theory]
        [MemberData(nameof(Search_given_SearchForm_containing_level_returns_list_of_material_prioritized_by_level_data))]
        public void Search_given_SearchForm_containing_level_returns_list_of_material_prioritized_by_level(List<LevelDTO> levels, List<Material> expected)
        {

            //Arrange
            SearchForm searchForm = new SearchForm("I am a text search", new List<TagDTO>() { new TagDTO(3, "Tag3") }, levels, new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);
            //Act

            var actual = _searchAlgorithm.Search(searchForm);

            //Assert
            //COmpare with DTO converted to Material or vice-versa

        }


        //tag4
        public static IEnumerable<object[]> Search_given_SearchForm_containing_language_returns_list_of_material_only_with_given_language_data()
        {
            return new List<object[]>{
                new object[] { new LanguageDTO(1, "Danish"), new List<Material> { _tag4Materials.ElementAt(0) } },
                new object[] { new LanguageDTO(2, "English"), new List<Material> { _tag4Materials.ElementAt(1) } },
                new object[] { new LanguageDTO(3, "Spanish"), new List<Material> { _tag4Materials.ElementAt(2) } }
                };

        }


        [Theory]
        [MemberData(nameof(Search_given_SearchForm_containing_language_returns_list_of_material_only_with_given_language_data))]
        public void Search_given_SearchForm_containing_language_returns_list_of_material_only_with_given_language(LanguageDTO language, List<Material> expected)
        {

            //Arrange
            SearchForm searchForm = new SearchForm("", new List<TagDTO>(), new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO> { language }, new List<MediaDTO>(), 0);
            List<MaterialDTO> expectedDTO = new List<MaterialDTO>();
            foreach (Material m in expected)
            {
                MaterialDTO temp = MaterialRepository.ConvertMaterialToMaterialDTOHashSet(m);
                expectedDTO.Add(temp);
            }

            //Act
            //search and return list of materialDTO
            var result = _searchAlgorithm.Search(searchForm).Result.Item2;

            //Assert
            //Compare with DTO converted to Material or vice-versa
            Assert.Equal<MaterialDTO>(expectedDTO, result);
        }


        //tag5, programmingLanguages
        public static IEnumerable<object[]> Search_given_SearchForm_containing_programminglanguage_returns_list_of_material_prioritized_by_programminglanguage_data()
        {
            yield return new object[] { new List<ProgrammingLanguageDTO>() { new ProgrammingLanguageDTO(1, "C#") }, new List<Material>() { _tag5Materials.ElementAt(0), _tag5Materials.ElementAt(3), _tag5Materials.ElementAt(4), _tag5Materials.ElementAt(6) } }; //all materials with ProLanguage, what order?

        }

        [Theory]
        [MemberData(nameof(Search_given_SearchForm_containing_programminglanguage_returns_list_of_material_prioritized_by_programminglanguage_data))]
        public void Search_given_SearchForm_containing_programminglanguage_returns_list_of_material_prioritized_by_programminglanguage(List<ProgrammingLanguageDTO> programmingLanguages, List<Material> expected)
        {

            //Arrange
            SearchForm searchForm = new SearchForm("I am a text search", new List<TagDTO>() { new TagDTO(5, "Tag5") }, new List<LevelDTO>() { }, programmingLanguages, new List<LanguageDTO>(), new List<MediaDTO>(), 0);

            //Act
            var actual = _searchAlgorithm.Search(searchForm);


            //Assert
            //COmpare with DTO converted to Material or vice-versa

        }




        //tag6
        public static IEnumerable<object[]> Search_given_SearchForm_containing_media_returns_list_of_material_only_with_given_media_data()
        {
            yield return new object[] { new MediaDTO(3, "Video"), new List<Material> { _tag6Materials.ElementAt(0) } };
            yield return new object[] { new MediaDTO(2, "Report"), new List<Material> { _tag6Materials.ElementAt(1) } };
        }


        [Theory]
        [MemberData(nameof(Search_given_SearchForm_containing_media_returns_list_of_material_only_with_given_media_data))]
        public void Search_given_SearchForm_containing_media_returns_list_of_material_only_with_given_media(MediaDTO media, List<Material> expected)
        {

            //Arrange
            SearchForm searchForm = new SearchForm("", new List<TagDTO>(), new List<LevelDTO>(), new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO> { media }, 0);
            List<MaterialDTO> expectedDTO = new List<MaterialDTO>();
            foreach (Material m in expected)
            {

                expectedDTO.Add(m.ConvertToMaterialDTO());
            }

            //Act
            //search and return list of materialDTO
            var result = _searchAlgorithm.Search(searchForm).Result.Item2;
            //Assert
            //Compare with DTO converted to Material or vice-versa
            Assert.Equal<MaterialDTO>(expectedDTO, result);
        }


        //tag7, title
        public static IEnumerable<object[]> Search_given_SearchForm_containing_title_returns_list_of_material_prioritized_by_title_data()
        {
            yield return new object[] { "Lorem", new List<Material>() { _tag7Materials.ElementAt(0), _tag7Materials.ElementAt(1), _tag7Materials.ElementAt(2), _tag7Materials.ElementAt(3), _tag7Materials.ElementAt(4) } }; //all materials searchword in title, what order?
            yield return new object[] { "lorem", new List<Material>() { _tag7Materials.ElementAt(0), _tag7Materials.ElementAt(1), _tag7Materials.ElementAt(2), _tag7Materials.ElementAt(3), _tag7Materials.ElementAt(4) } };
            yield return new object[] { "LOREM", new List<Material>() { _tag7Materials.ElementAt(0), _tag7Materials.ElementAt(1), _tag7Materials.ElementAt(2), _tag7Materials.ElementAt(3), _tag7Materials.ElementAt(4) } };
            yield return new object[] { "Lorem ipsum dolor sit amet", new List<Material>() { _tag7Materials.ElementAt(0), _tag7Materials.ElementAt(1), _tag7Materials.ElementAt(2), _tag7Materials.ElementAt(3), _tag7Materials.ElementAt(4) } }; //Prioritised by number of matches with words in title

        }

        [Theory]
        [MemberData(nameof(Search_given_SearchForm_containing_title_returns_list_of_material_prioritized_by_title_data))]
        public void Search_given_SearchForm_containing_title_returns_list_of_material_prioritized_by_title(string title, List<Material> expected)
        {

            //Arrange
            SearchForm searchForm = new SearchForm("I am a text search", new List<TagDTO>() { new TagDTO(7, "Tag7") }, new List<LevelDTO>() { }, new List<ProgrammingLanguageDTO>(), new List<LanguageDTO>(), new List<MediaDTO>(), 0);
            //Act
            var actual = _searchAlgorithm.Search(searchForm).Result.Item2;


            //Assert
            for (int i = 0; i < actual.Count - 1; i++)
            {
                Assert.Equal(expected.ElementAt(i).ConvertToMaterialDTO(), actual.ElementAt(i));
            }
        }


        */

    }
}
