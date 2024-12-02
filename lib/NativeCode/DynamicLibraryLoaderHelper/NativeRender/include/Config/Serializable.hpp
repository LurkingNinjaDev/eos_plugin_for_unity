#ifndef SERIALIZABLE_HPP
#define SERIALIZABLE_HPP

/*
 * Copyright (c) 2024 PlayEveryWare
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

#pragma once

#include <map>

#include "json.h"
#include "logging.h"
#include "string_helpers.h"

namespace pew::eos::config
{
    /**
     * \brief Used to describe the functions needed to make an object both
     * serializable and deserializable from JSON.
     */
    struct Serializable
    {
    protected:
        ~Serializable() = default;

        /**
         * \brief Function that is called when an element inside the json object
         * passed to from_json is being parsed.
         * \param name The name of the JSON element.
         * \param value The value of the JSON element.
         */
        virtual void parse_json_element(const std::string& name, json_value_s& value) = 0;

        ///**
        // * \brief Function that is called when an element inside the json object
        // * passed to from_json is being parsed. Default implementation ignores
        // * the iterator, but it's made virtual so that deriving classes can
        // * override it and make use of the iterator for circumstances where
        // * doing so is useful.
        // * \param iterator The iterator pointing to the indicated name and value.
        // */
        //virtual void parse_json_element(const json_object_element_s* iterator)
        //{
        //    parse_json_element(iterator->name->string, *(iterator->value));
        //}
        template<typename T>
        static std::enable_if_t<std::is_base_of_v<Serializable, T>, std::vector<T>> parse_json_array(json_value_s& array_value)
        {
            std::vector<T> elements;

            const auto array = json_value_as_array(&array_value);

            if (array == nullptr)
            {
                return elements;
            }

            auto array_iterator = array->start;

            while(array_iterator)
            {
                auto element = T();

                element.from_json(*(array_iterator->value));

                elements.push_back(element);

                array_iterator = array_iterator->next;
            }

            return elements;
        }

        template <typename T, typename = std::enable_if_t<
            std::is_same_v<T, int> || 
            std::is_same_v<T, double> || 
            std::is_same_v<T, long> ||
            std::is_same_v<T, float> ||
            std::is_same_v<T, uint64_t>
        >>
        static T parse_number(json_value_s& json_value)
        {
            // TODO: Add warning for if the value expected does not match the type expected
            T number_value;
            const auto number = json_value_as_number(&json_value);

            if constexpr (std::is_same_v<T, int>)
            {
                number_value = (number == nullptr) ? 0 : std::atoi(number->number);
            }
            else if constexpr (std::is_same_v<T, long>)
            {
                number_value = (number == nullptr) ? 0L : std::strtol(number->number, nullptr, 10);
            }
            else if constexpr (std::is_same_v<T, double>)
            {
                number_value = (number == nullptr) ? 0.0 : std::strtod(number->number, nullptr);
            }
            else if constexpr(std::is_same_v<T, float>)
            {
                char* end;
                number_value = (number == nullptr) ? 0.0f : std::strtof(number->number, &end);
            }
            else if constexpr(std::is_same_v<T, uint64_t>)
            {
                char* end;
                number_value = (number == nullptr) ? 0 : std::strtoull(number->number, &end, 10);
            }

            return number_value;
        }

        static bool parse_bool(const json_value_s& json_value)
        {
            if (json_value_is_true(&json_value))
            {
                return true;
            }
            else if (json_value_is_false(&json_value))
            {
                return false;
            }
            else
            {
                logging::log_warn("Value expected to be a boolean, but was neither true nor false. Setting to false.");
                return false;
            }
        }

        template<typename T, typename = std::enable_if_t< 
            std::is_same_v<T, int> ||
            std::is_same_v<T, EOS_EAuthScopeFlags> ||
            std::is_same_v<T, EOS_EIntegratedPlatformManagementFlags> ||
            std::is_same_v<T, EOS_UI_EInputStateButtonFlags>
            >>
        static T parse_flags(const std::map<std::string, T>* strings_to_enum_values, T default_value, json_value_s* value)
        {
            T flags_to_return = static_cast<T>(0);
            bool flag_set = false;

            // TODO: Deal with null case.
            const std::string flags_str = json_value_as_string(value)->string;

            const std::vector<std::string> string_values = string_helpers::split_and_trim(flags_str);

            // Iterate through the string values
            for (const auto str : string_values)
            {
                // Skip if the string is not in the map
                if (strings_to_enum_values->find(str.c_str()) == strings_to_enum_values->end())
                {
                    continue;
                }

                // Otherwise, append the enum value
                flags_to_return |= strings_to_enum_values->at(str.c_str());
                flag_set = true;
            }

            return (flag_set) ? flags_to_return : default_value;
        }

    public:

        virtual void from_json(const json_value_s& json_value)
        {
            // Convert the value to json object
            const auto json_object = static_cast<json_object_s*>(json_value.payload);
            // Use the object to parse
            from_json(*json_object);
        }

        /**
         * \brief Parses each element of the json object that is provided.
         * \param json The json object.
         */
        virtual void from_json(const json_object_s& json)
        {
            auto json_object_iterator = json.start;

            // While there are still items to parse
            while(json_object_iterator)
            {
                auto element = *(json_object_iterator->value);
                const std::string element_name = json_object_iterator->name->string;

                // Use the deriving class' parse_json_element function to parse
                // the value.
                parse_json_element(json_object_iterator->name->string, *(json_object_iterator->value));

                if (json_object_iterator == nullptr)
                {
                    break;
                }

                // Move to the next element
                json_object_iterator = json_object_iterator->next;
            }
        }
    };
}

#endif
#pragma once
