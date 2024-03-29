# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.8.7] - 2024-03-29

### Added
- `Timer.Progress`
- `HasFlag` for some integral types
- `Gameobject.HasComponent()`

## [1.8.6] - 2024-03-18

### Changed
- Removed the `readonly` modifiers on the serialized private fields

## [1.8.5] - 2024-03-14

### Added
- More Utils functions (remaps variants, IOUtils)
- `TagAttribute`
- The first sample, containing a .editorconfig file

## [1.8.4] - 2024-03-11

### Added
- `Disable` and `DisableIf` attributes
- UIElements support for `MinMaxSlider` attribute
- Font size option for the `Title` and `Label` attributes
- `Vectors.SqrDistance`

## [1.8.3] - 2024-03-10

### Added
- More Utils

### Modified
- Better `Timer`
- Utils methods inlining
- Better `Axis` to `Vector3` conversion method

## [1.8.2] - 2024-02-29

### Added
- `LabelAttribute` and `SeparatorAttrbute`
- `Vector.CopyScale`

## [1.8.1] - 2024-02-24

### Added
- `Axis` to `Vector3` conversion method

## [1.8.0] - 2024-02-24

### Added
- More Utils methods
- A `Timer` class
- More recorder classes
- An implementation of the OneEuro Filter and a LowPass Filter

## [1.7.4] - 2024-02-24

### Modified
- Fixed compile error due to an ambiguity

## [1.7.3] - 2024-02-24

### Added
- Added an `Axis` enum

## [1.7.2] - 2024-02-23

### Changed
- Fixed `EditorUtils.IsPropertyPartOfArray`

## [1.7.1] - 2024-02-11

### Added
- `Layer` Attribute

### Changed
- Minor improvement on Component pools
- Renamed singletons base classes
- Fixed `ScriptableSingleton` behaviour
- Fixed `MinMaxSliders` multi editing

## [1.7.0] - 2024-02-11

### Added
- Script execution time recorder

## [1.6.0] - 2024-02-11

### Added
- The first part of a collection of utility methods and class extensions

## [1.5.1] - 2024-01-20

### Added
- Made the package buildable

## [1.5.0] - 2024-01-20

### Added
- `Button` Attribute

## [1.4.0] - 2024-01-10

### Added
- Pools

## [1.3.0] - 2024-01-10

### Added
- 4 custom PropertyAttributes and drawers :
  - `ShowIf`
  - `MinMaxSlider`
  - `Title`
  - `HelpBox`
  
## [1.2.0] - 2024-01-10

### Added
- `ManagedMonoBehaviour`

## [1.1.0] - 2024-01-10

### Added
- `SerializedDictionary`

## [1.0.0] - 2024-01-10

### Added
- Singleton's base clases
- Scene References by JohannesMP (<https://gist.github.com/JohannesMP/ec7d3f0bcf167dab3d0d3bb480e0e07b>)
